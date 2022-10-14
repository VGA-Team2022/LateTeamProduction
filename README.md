# 後期チーム制作開発録

## 制作メンバー
* 発案者 __GK22 内田__
* ディレクター __PS21A 新井__
* プランナー __GK22 鈴木__
  * GK22 石川 廣瀬, PA20 大山, PS21C 川島
* プログラマ __PS21A 澤田__
  * PS21A 戸澤 上田 成田, PS21B 岩前 楠原 平石 宮川 矢作
* デザイナー ____
  * CS21A 北口 藤田, CS21B 梅原 小野原 川西 鶴田 本橋 山本 阿部 金田 黒川 三好, CS21C 赤池 星野, CD22A 小栗

## 制作前課題
* 制作における分業をどうするか
  * タスク/進捗の管理を各役職ごとに行う？
* ゲームをどの方向に広げるか
  * 2Dドットにすれば不自然さをぼかせそう？
* ステージ/ギミックに関して
  * マップ自動生成に関して
    * ステージギミックに何を伸ばすか
  * 枕返しのギミックに関して
    * 原案の中でもブレさせないで遊びに紐付ける

## 作業スケジュール
* 遊びの検証
  * 1部屋
  * 枕をひっくり返す機能を作る
  * １部屋に数人寝ていて枕返しのリザルト(True/False)パターンを実装する  
1. 枕を返すギミックの作成
2. 廊下の仲居のギミックの作成
3. マップの生成ギミックの作成

### プログラマ作業分担表
__1. プレイヤー：__
  * 移動
  * イベントフラグを送る

__2. 徘徊する敵：__
  * 移動
  * プレイヤー発見時の処理

__3. UIロジック：__
  * UI周りの動き

__4. ステージ上の要素：__
  * 枕返しのギミック
  * 大人と子供の変身ギミック
  * 徘徊する敵の順路変更ギミック

__5. マネージャー関係：__
  * ゲームマネージャー
  * サウンドマネージャー
  * シーンマネージャー

__6. マップ自動生成：__
  * マップ生成のクラス作成 

### デザイナー主人公案
<img width="600" alt="PlayerPlan011" src="https://github.com/Ryuki-Arai/LateTeamProduction/blob/main/REDME_Picture/Designer_Plan_Player.png">
