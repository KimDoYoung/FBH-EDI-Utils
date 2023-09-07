# EdiDiff

## 개요

1. 850을 중심으로 945에서 품목을 찾아서 갯수가 맞는지 체크한다.
2. 입력이 되는 850, 945 리스트 엑셀파일은 EdiListMaker(EdiUtils)에서 만들어진 파일이어야한다.
3. 체크한 항목들을 보여주는 excel을 만든다.
4. 945에서는 po + item no(retail's item no)로 찾는다.
5. 찾은 결과는 1) 발견못함 2)일치 3) 불일치 3개로 보여준다.

> icon 딸기

## invoice 특징

1.  invoice list.xslx와  RL invoice list.xslx의 PO#별 금액을 비교하여 invoice check list.xlsx 생성

2. invoice check list.xlsxd의 월마트 PO 발주일 / Invoice 상신일(FBH) / TOTAL (USD) / PO# 는 invoice list.xslx에서 가져오는 값

3. invoice check list.xlsx의 Check Date / Amount / Status 는 RL invoice lis.xslxt에서 가져오는 값

4. RL invoice list.xslx의 Invoice/Claim Number에서 뒤의 3자리 숫자를 지우면 PO#
예시: 9534186812272 -> 뒤의 272 제거 -> 9534186812가 PO#


일치: RL invoice list.xslx 금액과 invoice list.xslx 금액이 같다
불일치: RL invoice list.xslx 금액과 invoice list.xslx 금액이 다르다
N/A: RL invoice list.xslx와 invoice list.xslx에 일치하는 데이터가 없다
N/D: Check Date가 없다




## 개발시 필요한 패키지

- NuGet Package Manager에서  **Microsoft.Office.Interop.Excel** 찾아서 설치해야한다.
- [엑셀에 필요한 라이브러리](https://www.freecodespot.com/blog/csharp-import-excel/)


## History

- 2023-09-6 : invoice vs RL invoice 체크 기능 추가 

