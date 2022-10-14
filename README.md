# procon33
## それぞれのファイルについて
## isolation
マルチチャンネル時変ガウスモデルを用いて音源分離をするプログラム
## procon33_gui
C#にて書かれたプロコン用のGUIツール
## scripts
### 音声に関するスクリプトについて
### search_part.py
音声の相関を調べるスクリプト
### テスト用のスクリプトについて
### makequal.py
入力を受けた音声を重ねるスクリプト
### setup.py
音源データがなければダウンロードをして、num変数の数字の数分の音声データをmakequal.py使って合成するスクリプト
### test_case.py
search_part.pyを実行して結果をall_result.pyに入れるスクリプト
### test.sh
setup.pyとtest_case.pyを指定の回数繰り返すスクリプト
### collect.py
all_resultの結果を集計するスクリプト
