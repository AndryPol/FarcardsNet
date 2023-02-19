# FarcardNet

��� ������� ��� ���������� ����������� ������ ������� �� ����� c# 
��� ��������� ��� ��� ��� �������� � �������� ���� 
��� ���������� ����� � ���������� ��������� Farcard �������� Ucs(Rkeeper)

������ ������� �������� � ����� � ��������������� ��������� �� ��������� ����� ��������� � ��� �� �� �� ������������ � ������ ������ ������������

# ��� ��� ��������:
				    |    FarcardContract       |
				    |      /        \	       |
	|����� Rkeeper|->|Farcard|->|FarcardNet|->|������ �� c#| 


---
[�������� ����������� ������������](https://docs.rkeeper.ru/crm/nastrojka-farcards-dlya-vneshnej-sistemy-loyal-nosti-51413658.html)  
��� �� � ������� FarcardContract ��������� �����:  
Extdll5.txt �������� �������������� � Farcard 5 ������(���� �� �����������)  
Extdll6.txt �������� �������������� � Farcard 6 ������  
TRRESPONCE.txt �������� ������� � ���� xml �� ���������� ���������� � ������� TransactionsEx  
��� �� �������� ����� ���������� � �������� ���������� ����� ���������� � ���������������� ������������

---
������� �������� ��������� ��������:

## 1 Far�ardContract
 ��� ������ ���������� c# ������� � ����:
- ��� ����������� ��������� ������� ������ ��� ������������� � ��������.
- �������� ���������� �� ����� ��������� ������������ � Farcard6 (����� ���������� ��� ������ ������)
- ������� ��� �������� ������� �� ����� ���������� ����������� ������(������������ ���������� Mef)
- �������� ��� ������������� � Farcard6 (����� ���������� ��� ������ ������)
- �������������� ������� DTO ��� ������ �� Http
- ������������� ������ ������� � �������� ��� ���������� ��������
- ���������� ���������� � ���� Demo �������
- �������������� ����������
---
## 2 FarcardNet
��� ������ ���������� �� �++ ������� ������������ � Farcard ��� ���������� ExtDll (���� ����������� ��� ������ 6)  
� ���������� ������ ���������� ����������� ���� �� ������� ����������� �� c# � ���������� ������������ �� ���������� FarcardContract 
  
  ---
## 3 Farcards
��� ������ ��������� ���������� ��� �������� � ���������� �������� ������� � ��������(������ ��� ������� �������� �������� ��� ���������) 
## 4 HttpFarcardService 
��� ������ ������ ��� ��������� �������� �� Http � ���� Xml � json � ��������������� �� � ������� � ���������� ������������ �� ���������� FarcardContract  
������ ����� ��������� � ���������� ������ ���� ���������� � �������� ������ Windows  
��� ��������� ��� �������� ����� ��������� ���������� � ����������� (-i ��� -install ��� ���������) ��� (-u ��� -uninstall ��� ��������) 
��� �� ����� ������� �������������� �������� ������ � ���������� -n ��� -name 

������� � ������ ��� ������ � ������� xml [������� ���](https://docs.rkeeper.ru/crm/http-protokol-dlya-farcards-19611635.html)  
!!!����� ����������� �� ��� ������� � ������:
 - 1.GetCardInfoEx
 - 2.TransactionsEx
 - 3.GetCardImageEx
 - 4.FindEmail

 O�������� ������ � �������� ����������
 �������� �������� � ���� json:
 
 ---
 - 1.GetCardInfoEx Post 
 >������ ���� ������a:
 {"Card":1,"Restaurant":1,"UnitNo":1,"InpBuf":null,"InpKind":0}  
 
���  
Card - ��� ����� ��� ����� (int64)  
Restaurant - ��� ��������� ��� ����� (UInt32)  
UnitNo - ����� ����� ��� ����� (UInt32)  
InpBuf - ������ ���������� �� ����� � ������� Base64 �������� ��� (������ ����)  
InpKind - ��� ������������ ���������� �� �����
> ������ ���� ������:
{"Result":0,"CardInfo":{"Sieze":0,"Deleted":0,"Grab":0,"StopDate":0,"Holy":0,"Manager":0,"Locked":0,"WhyLock":"","Holder":"�������� ������","PersonID":1234,"Account":1234,"Unpay":0,"Bonus":5,"Discount":99,"DiscLimit":900000.0,"Summa":1000.05,"Sum2":200.0,"Sum3":300.1,"Sum4":400.0,"Sum5":500.0,"Sum6":600.0,"Sum7":700.0,"Sum8":800.0,"DopInfo":"�������� ����������","ScrMessage":"�������� ���������� ��� ������","PrnMessage":"�������� ���������� ��� ������"},"OutBuf":null,"OutKind":0}

���  
Result - ��������� �������, 0 ����� ������� 1 ����� �� �������  
CardInfo - ��� ������ ���������� �� ����� �������� �������� � ����� Extdll6.txt ������� FarcardContract ������� GetcardInfoEx  
OutBuf - ������ ���������� ��� ����� � ������� Base64 �������� ��� (������ ����)  
OutKind - ��� ������������ ���������� ��� �����

---
- 2 TransactionsEx Post
>������ ���� �������:
{"Transactions":[{"Sieze":0,"Card":1,"PersonID":1011,"Account":1001,"Kind":1,"Summa":100.0,"Restaurant":1,"RKDate":"2023-02-12T00:00:00","RKUni":1,"RKCheck":1,"VatSumA":0.0,"VatPrcA":0.0,"VatSumB":0.0,"VatPrcB":0.0,"VatSumC":0.0,"VatPrcC":0.0,"VatSumD":0.0,"VatPrcD":0.0,"VatSumE":0.0,"VatPrcE":0.0,"VatSumF":0.0,"VatPrcF":0.0,"VatSumG":0.0,"VatPrcG":0.0,"VatSumH":0.0,"VatPrcH":0.0},{"Sieze":0,"Card":2,"PersonID":1012,"Account":1002,"Kind":2,"Summa":100.0,"Restaurant":1,"RKDate":"2023-02-12T00:00:00","RKUni":1,"RKCheck":1,"VatSumA":0.0,"VatPrcA":0.0,"VatSumB":0.0,"VatPrcB":0.0,"VatSumC":0.0,"VatPrcC":0.0,"VatSumD":0.0,"VatPrcD":0.0,"VatSumE":0.0,"VatPrcE":0.0,"VatSumF":0.0,"VatPrcF":0.0,"VatSumG":0.0,"VatPrcG":0.0,"VatSumH":0.0,"VatPrcH":0.0}],"InpBuf":null,"InpKind":0}

���  
Transactions - ������ �������� ����������� ���� ����������  
InpBuf - ������ ���������� �� ����� � ������� Base64 �������� ��� (������ ����)  
InpKind - ��� ������������ ���������� �� �����

>������ ���� ������:
{"Result":0,"OutBuf":null,"OutKind":0}
  
���  
Result - ��������� �������, 0 ��� ���������� ��������� 1 ��� ���������� ����� �� ���������� ��������� ������  
OutBuf - ������ ���������� ��� ����� � ������� Base64 �������� ��� (������ ����)  
OutKind - ��� ������������ ���������� ��� �����

---
- 3 GetCardImageEx Post  
�������������� ����������� BMP, JPEG, GIF.
>������ ���� �������:
{"CardCode":1}

���  
CardCode ��� ����� ��� ����� (int64)

>������ ���� ������ ��� ������:
{"ErrorText":"Card or image not found"}

���  
ErrorText �������� ������

� ������ ��������� ������ ��� �������� ����� �� json � (image/[bmp,jpg or jpeg,gif]) 

---
 - 4.FindEmail Post
 >������ ���� �������:
{"Email":"test@test.ru"}

���  
Email ������ ���������� email ��� ������

>������ ���� ������:
{"Result":0,"Account":1234,"CardCode":1,"Name":"�������� ������"}  

���  
Result - ��������� �������, 0 ����� ������� 1 ����� �� �������  
Account - ����� ����� ������� ��� ���������� ����������
CardCode - ����� ����� �������
Name - ��� �������

---
����� ����������� 2 ������� ��� ������� ���������� ��������
## TunnelFarcard6 
������ ������� ��� ������������ ExtDll ��� ������������� ��� ������������ ��������� � ����������� ���������

---
## HttpFarcard6Client
������ ����������� ������ ��������� FarcardHttp, ��� ������� � �������� ��������� ������� ��� ��������� � ������� HttpFarcardService
�������� � ���� ������� ������� ��� � Xml ���� ��� � � json 
