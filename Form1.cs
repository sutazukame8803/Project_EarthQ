using System.Diagnostics.Eventing.Reader;
using System.Drawing.Text;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EarthQ
{
    public partial class Main : Form
    {
        private float _angle = 0f;
        private ClientWebSocket _webSocket = new ClientWebSocket();

        public Main()
        {
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                await _webSocket.ConnectAsync(new Uri("wss://api.p2pquake.net/v2/ws"), CancellationToken.None);
                lastupdate.Text = "接続成功！データ待機中...";
                _ = Task.Run(ReceiveLoop);
            }
            catch (Exception ex)
            {
                lastupdate.Text = "接続失敗: " + ex.Message;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.Aqua, Color.DarkGray, _angle))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

        }

        private async Task ReceiveLoop()
        {
            var buffer = new byte[1024 * 64];
            while (_webSocket.State == WebSocketState.Open)
            {
                try
                {
                    var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        this.Invoke(new Action(() =>
                        {

                            ParseAndDisplay(message);
                        }));
                    }
                }
                catch (Exception ex)
                {
                    // 受信エラーを無視してループを続ける
                    this.Invoke(new Action(() =>
                    {
                        subinfo.Text = "受信中... (エラー: " + ex.Message + ")";
                    }));
                }
            }
        }

        private void ParseAndDisplay(string json)
        {
            try
            {
                // 1. JSON文字列を解析可能なドキュメント形式に変換
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    JsonElement root = doc.RootElement;
                    MainInfo.Text = json;
                    lastupdate.Text = "最終更新: " + DateTime.Now.ToString("HH:mm:ss");

                    // 2. JSONから "code" という項目を探す
                    if (root.TryGetProperty("code", out var codeElement))
                    {
                        int code = codeElement.GetInt32(); // コード番号を整数として取得

                        // 3. コード番号ごとに処理を分岐（スイッチ文でスッキリ管理）
                        switch (code)
                        {
                            case 551: // 地震情報受信時の処理
                                PlayQuakeSound();
                                if (root.TryGetProperty("earthquake", out var eq))
                                {
                                    // 発生時刻とマグニチュードを抽出
                                    string time = eq.GetProperty("time").GetString();
                                    string mag = eq.TryGetProperty("hypocenter", out var hypo) ? hypo.GetProperty("magnitude").ToString() : "不明";

                                    lastupdate.Text = "発生時刻: " + time;
                                    magnitude.Text = "マグニチュード: " + mag;
                                }
                                break;

                            case 555: // 各地の揺れ（震度情報）受信時の処理
                                var areas = root.GetProperty("areas");
                                int maxPeer = 0;

                                // 全エリアをループして一番報告数(peer)が多い場所を探す
                                foreach (var area in areas.EnumerateArray())
                                {
                                    int peer = area.GetProperty("peer").GetInt32();
                                    if (peer > maxPeer) maxPeer = peer;
                                }

                                // 報告数に応じて背景色を変えて警告レベルを表現
                                if (maxPeer >= 100) { subinfo.BackColor = Color.Red; subinfo.Text = "🚨 警戒: 強い揺れ！ (" + maxPeer + ")"; }
                                else if (maxPeer >= 50) { subinfo.BackColor = Color.Yellow; subinfo.Text = "⚠️ 注意: 揺れあり (" + maxPeer + ")"; }
                                else if (maxPeer >= 20) { subinfo.BackColor = Color.Cyan; subinfo.Text = "揺れ感知 (" + maxPeer + ")"; }
                                else { subinfo.BackColor = Color.Transparent; subinfo.Text = "揺れ報告数: " + maxPeer; }
                                break;

                            default: // その他のコードが来た場合のフォールバック
                                subinfo.Text = "その他のデータを受信中: " + code;
                                subinfo.BackColor = Color.Transparent;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 解析中にエラーが起きてもアプリを落とさないためのガード
                subinfo.Text = "解析エラー: " + ex.Message;
            }
        }
        private void PlayQuakeSound()
        {
            System.Media.SystemSounds.Hand.Play();
        }
        private void subinfo_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            subinfo.ForeColor = Color.Black;
            if (_webSocket.State != WebSocketState.Open)
            {
                subinfo.Text = "再接続中";
                _webSocket = new ClientWebSocket();
                try
                {
                    await _webSocket.ConnectAsync(new Uri("wss://api.p2pquake.net/v2/ws"), CancellationToken.None);
                    _ = Task.Run(ReceiveLoop);
                }
                catch (Exception ex)
                {
                    subinfo.Text = "接続失敗" + ex.Message;
                }
            }
            else
            {
                subinfo.Text = "接続OK!(" + DateTime.Now.ToString("HH:mm:ss") + ")";
            }

        }

        private void lastupdate_Click(object sender, EventArgs e)
        {

        }
        private void magnitude_Click(object sender, EventArgs e) { }
        private void subinfo_Click_1(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _angle += 10f;
            if (_angle >= 360f) _angle = 0f;
            this.Invalidate();
        }
    }
}