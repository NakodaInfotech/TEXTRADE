DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @TOTALMTRS DECIMAL(18,2)
SELECT @TOTALMTRS = SUM(GRN_TOTALMTRS) FROM  GRN LEFT OUTER JOIN LEDGERS ON GRN.GRN_TOLEDGERID = LEDGERS.Acc_id  WHERE (GRN.GRN_TYPE = 'Job Work') AND (ISNULL(GRN.GRN_TOLEDGERID, 0) = 0 OR
                         ISNULL(LEDGERS.Acc_cmpname, '') = 'AVIS TRANSPORT') AND (GRN.grn_date < CAST(GETDATE() AS DATE)) 
						 AND GRN.GRN_YEARID = (SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = 'AVIS INDUSTRIES PVT. LTD.' ORDER BY YEAR_STARTDATE DESC)

SET @SUBJECT = 'DAILY GREY STOCK REPORT FOR ' + CAST(DAY( GETDATE()) AS VARCHAR(2)) + '-' + CAST(MONTH( GETDATE()) AS VARCHAR(2)) + '-' + CAST(YEAR( GETDATE()) AS VARCHAR(4))
DECLARE @BODY VARCHAR(MAX)
SET @BODY = ''


DECLARE @ITEMNAME AS VARCHAR(100), @ITEMMTRS FLOAT
declare CUR_MAIN cursor for select ITEMMASTER.ITEM_NAME, SUM(GRN_MTRS) AS ITEMMTRS from GRN INNER JOIN GRN_DESC INNER JOIN ITEMMASTER ON GRN_DESC.GRN_ITEMID = ITEMMASTER.item_id ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.GRN_TYPE = GRN_DESC.GRN_GRIDTYPE AND  GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN LEDGERS ON GRN.GRN_TOLEDGERID = LEDGERS.Acc_id WHERE (GRN.GRN_TYPE = 'Job Work') AND (ISNULL(GRN.GRN_TOLEDGERID, 0) = 0 OR ISNULL(LEDGERS.Acc_cmpname, '') = 'AVIS TRANSPORT') AND GRN.GRN_YEARID = (SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = 'AVIS INDUSTRIES PVT. LTD.' ORDER BY YEAR_STARTDATE DESC)  GROUP BY ITEMMASTER.ITEM_NAME
open CUR_MAIN
	fetch next from CUR_MAIN into @ITEMNAME, @ITEMMTRS
	while @@Fetch_STATUS = 0
		begin

			SET @ENTRYBODY = (SELECT  
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= PARTYLEDGERS.Acc_cmpname ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= ITEMMASTER.item_name ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=CAST(GRN_DESC.GRN_MTRS AS DECIMAL(18,2)) ,'',
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=CAST(ISNULL(PURCHASEMASTER_DESC.BILL_rate,0) AS DECIMAL(18,2)) ,'',
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=GRN.grn_lrno ,''
			FROM GRN LEFT OUTER JOIN
				LEDGERS ON GRN.GRN_TOLEDGERID = LEDGERS.Acc_id INNER JOIN
				LEDGERS AS PARTYLEDGERS ON GRN.grn_ledgerid = PARTYLEDGERS.Acc_id INNER JOIN
				GRN_DESC INNER JOIN
				ITEMMASTER ON GRN_DESC.GRN_ITEMID = ITEMMASTER.item_id ON GRN.grn_no = GRN_DESC.GRN_NO AND GRN.GRN_TYPE = GRN_DESC.GRN_GRIDTYPE AND 
				GRN.grn_yearid = GRN_DESC.GRN_YEARID LEFT OUTER JOIN
				PURCHASEMASTER_DESC ON GRN.GRN_TYPE = PURCHASEMASTER_DESC.BILL_TYPE AND GRN.grn_yearid = PURCHASEMASTER_DESC.BILL_yearid AND 
			GRN.grn_no = PURCHASEMASTER_DESC.BILL_GRNNO
			WHERE ITEMMASTER.ITEM_NAME = @ITEMNAME AND (GRN.GRN_TYPE = 'Job Work') AND (ISNULL(GRN.GRN_TOLEDGERID, 0) = 0 OR
			ISNULL(LEDGERS.Acc_cmpname, '') = 'AVIS TRANSPORT') AND GRN.GRN_YEARID = (SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = 'AVIS INDUSTRIES PVT. LTD.' ORDER BY YEAR_STARTDATE DESC)



			FOR XML PATH ('tr'))


			SET @BODY = @BODY + N'<H1 style="font-family:Tahoma; font-size:11px;">' +  @ITEMNAME + '</H1>' +
							N'<Table Border = "1">' + 
							N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Name</Th><Th>Item Name</Th><Th>Mtrs</Th><Th>Rate</Th><Th>LR No</Th></Tr>'+
							+ISNULL(@ENTRYBODY,'')+ N'<tfoot><tr><td colspan="2"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">ITEM TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@ITEMMTRS AS VARCHAR) + N'</td>
							</tr></tfoot></Table></html>'


			fetch next from CUR_MAIN into @ITEMNAME, @ITEMMTRS
		end
close CUR_MAIN
deallocate CUR_MAIN




SET @BODY = @BODY + N'<Table Border = "1">' + 
							 N'<tfoot><tr><td colspan="2"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="lightgreen">GRAND TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="lightgreen">' + CAST(@TOTALMTRS AS VARCHAR) + N'</td>
							</tr></tfoot></Table></html>'




DECLARE @FINALBODY VARCHAR(MAX)
SET @FINALBODY = @BODY 


EXEC msdb.dbo.sp_send_dbmail
@profile_name='TEXTRADEMAIL',
@recipients= 'infoavisindustries@gmail.com;gm@avisindustries.in',	
@subject=@SUBJECT,
@body=@FINALBODY,
@body_format = 'HTML'