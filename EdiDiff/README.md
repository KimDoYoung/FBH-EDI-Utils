# EdiDiff

## 개요

1. 850을 중심으로 945에서 품목을 찾아서 갯수가 맞는지 체크한다.
2. 입력이 되는 850, 945 리스트 엑셀파일은 EdiListMaker(EdiUtils)에서 만들어진 파일이어야한다.
3. 체크한 항목들을 보여주는 excel을 만든다.
4. 945에서는 po + item no(retail's item no)로 찾는다.
5. 찾은 결과는 1) 발견못함 2)일치 3) 불일치 3개로 보여준다.

## hub 210 invoice

1. excel과 pdf 두가지 소스로 리스트를 만든다.
2. excel에서 만든 리스트와 pdf로 만든 리스트 두 개를 손으로 합친다.
3. sheet1에는 excel에서 만든 리스트, shee2에는 pdf에서 만든 list를 두고  중복제거

### hub 210 invoice 테스트

1. shee1에 중복된 라인
2. sheet1과 sheet2에 중복된 라인
3. sheet1과 sheet2에 각각 1개씩 존재
4. sheet2의 PO#에 문자열에 포함된 po와 sheet1의 po가 같은 경우

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

- 2023-09-06 : invoice vs RL invoice 체크 기능 추가 
- 2023-09-08 : ver 2.0.0.0  hub 210 합치는 기능 추가
- 2023-09-13 : ver 2.1.0.0  hub 201 합치때 소스를 Listmaker에서 만든 것으로 통일
- 2023-09-14 : ver 2.2.0.0  invoicedate + invoiceno로 중복을 체크함.
- 2023-09-26 : ver 2.3.0.0  DC별로 소팅함. (같은 DC인데 물류비가 많이 나가는 것 구하기 위해서)

