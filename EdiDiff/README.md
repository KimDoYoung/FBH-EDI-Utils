# EdiDiff

## ����

1. 850�� �߽����� 945���� ǰ���� ã�Ƽ� ������ �´��� üũ�Ѵ�.
2. �Է��� �Ǵ� 850, 945 ����Ʈ ���������� EdiListMaker(EdiUtils)���� ������� �����̾���Ѵ�.
3. üũ�� �׸���� �����ִ� excel�� �����.
4. 945������ po + item no(retail's item no)�� ã�´�.
5. ã�� ����� 1) �߰߸��� 2)��ġ 3) ����ġ 3���� �����ش�.

> icon ����

## invoice Ư¡

1.  invoice list.xslx��  RL invoice list.xslx�� PO#�� �ݾ��� ���Ͽ� invoice check list.xlsx ����

2. invoice check list.xlsxd�� ����Ʈ PO ������ / Invoice �����(FBH) / TOTAL (USD) / PO# �� invoice list.xslx���� �������� ��

3. invoice check list.xlsx�� Check Date / Amount / Status �� RL invoice lis.xslxt���� �������� ��

4. RL invoice list.xslx�� Invoice/Claim Number���� ���� 3�ڸ� ���ڸ� ����� PO#
����: 9534186812272 -> ���� 272 ���� -> 9534186812�� PO#


��ġ: RL invoice list.xslx �ݾװ� invoice list.xslx �ݾ��� ����
����ġ: RL invoice list.xslx �ݾװ� invoice list.xslx �ݾ��� �ٸ���
N/A: RL invoice list.xslx�� invoice list.xslx�� ��ġ�ϴ� �����Ͱ� ����
N/D: Check Date�� ����




## ���߽� �ʿ��� ��Ű��

- NuGet Package Manager����  **Microsoft.Office.Interop.Excel** ã�Ƽ� ��ġ�ؾ��Ѵ�.
- [������ �ʿ��� ���̺귯��](https://www.freecodespot.com/blog/csharp-import-excel/)


## History

- 2023-09-6 : invoice vs RL invoice üũ ��� �߰� 

