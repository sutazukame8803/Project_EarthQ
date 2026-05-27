<img width="800" height="480" alt="image" src="https://github.com/user-attachments/assets/6e8ca4fa-6a5d-4662-b578-5ee603a8a2d0" /># Project_EarthQ

sutazukame8803の地震情報アプリです。
このアプリは [P2P地震情報](https://api.p2pquake.net/) のAPIを使用して製作されています。APIを提供してくださっている皆様に深く感謝いたします。

## 機能 (Features)
* 地震発生時にリアルタイムで情報を受け取り、マグニチュードや発生時刻を表示します。
* 揺れの報告数（peer）を監視し、一定以上の報告がある場合に画面の警告レベルを切り替えます。
* 背景には動的なグラデーションアニメーションを採用しています。
<img width="800" height="480" alt="image" src="https://github.com/user-attachments/assets/4da5d516-f6df-4527-9e6a-1821441249c2" />
## 使い方 (Usage)
1. アプリを起動します。
2. 自動的にP2P地震情報のWebSocketへ接続されます。
3. 地震情報が更新されると、画面に詳細が表示されます。
4. [更新ボタン] を押すと、手動で最新の状態にリセットできます。
5. メモリは100MBほどです。

## ライセンス (License)
MIT License
