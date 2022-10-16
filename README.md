# procon33
## それぞれのファイルについて
## procon33_gui
C#にて書かれたプロコン用のGUIツール
## scripts
### テスト用のスクリプトについて
### makequal.py
入力を受けた音声を重ねるスクリプト
### setup.py
音源データがなければダウンロードをして、num変数の数字の数分の音声データをmakequal.py使って合成するスクリプト
### test_case.py
search_part.pyを実行して結果をresult.pyに入れるスクリプト
## 問題を解くためのスクリプト
### isolation.py
音声を分離するスクリプト
### procon33_system.py
高専プロコンのシステムと通信する用のスクリプト
GUIから呼び出しています。
### collect.py
result.txtの結果を集計するスクリプト
### search_part.py
相関関係を調べるスクリプト
### search_part_split.py
始める位置や読みデータから切り取るサンプル数などのパラメータを変えながら相互相関関数の値を調べるスクリプト
