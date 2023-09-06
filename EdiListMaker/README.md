# EdiUtils

## 개요

- 에프비홀딩스의 invoice파일들과 발주서 파일들로부터 업무에 필요한 엑셀파일을 만든다.
- COM베이스로 엑셀파일을 다룬다. 즉 엑셀파일읽기, 새로 만들기등

1. 850 PO (월마트 발주서) 폴더: PO 예시
- 각각의 PO 파일로 '850 List of W27 PO' 파일을 작성합니다.
- 참고사항: GLN의 경우 앞에 숫자 00이 꼭 붙어야 합니다.

2. 810 Invoice (월마트 인보이스) 폴더: Invoice 예시
- 810 Invoice 파일을 기반으로 810 List 를 작성합니다.
- 참고사항:
  Invoice 발행일(Wal-mart) = Invoice 상신일(FBH) +3
  예상 입금일(day) = Invoice 발행일(Wal-mart) + 35
  Order (pack): 해당 PO의 주문 총 수량

3. po 850 파일들 각각을 invoice 810 각각으로 만듬.
   - invoiceno를 만들데 년중 주차수를 구하는데. 이것을 테이블로 처리함.
4. 210 각 파일들을 읽어서 리스트 파일을 만듬
5. 945 각 파일들을 읽어서 리스트 파일을 만듬

## 빌드 전 event
```
copy /Y $(ProjectDir)Resources\template_850_List2.xlsx $(TargetDir)template_850_List2.xlsx
copy /Y $(ProjectDir)Resources\WeekOfYear-Kroger.csv $(TargetDir)WeekOfYear-Kroger.csv
copy /Y $(ProjectDir)Resources\EdiUtils.config $(TargetDir)EdiUtils.config

```
## 개발

- NuGet Package Manager에서  **Microsoft.Office.Interop.Excel** 찾아서 설치해야한다.
- [엑셀에 필요한 라이브러리](https://www.freecodespot.com/blog/csharp-import-excel/)


## history

- 2023-08-22 : 850->810 각각 만드는 기능을 추가했슴
- 2023-08-23 : 매출표를 위한 통계기능
  * PO List에 항목추가
  * Wallmart뿐만 아니라 Krogger, WM.COM도 해석
- 2023-08-24 : 버젼 1.3.0.0  invoice 210 각 excel을 목록 만드는 기능
- 2023-09-04 : 버젼 1.4.0.0 skip, 945리스트, 매출표 수정
- 2023-09-05 : 버젼 1.6.0.0 매출표 계산 오류 수정
 
