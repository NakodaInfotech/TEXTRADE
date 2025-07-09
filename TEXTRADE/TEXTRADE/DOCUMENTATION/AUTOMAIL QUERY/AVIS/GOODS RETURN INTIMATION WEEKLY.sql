DECLARE @SUBJECT VARCHAR(100), @ENTRYBODY VARCHAR(MAX), @TOTALMTRS DECIMAL(18,2), @CMPNAME AS VARCHAR(100), @YEARID INT
SET @CMPNAME = 'SUPRIYA SILK MILLS PVT. LTD.'
SET @YEARID=(SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = @CMPNAME  and year_startdate= CASE WHEN MONTH(GETDATE()) > 3 THEN CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-04-01' ELSE CAST(YEAR(GETDATE())-1 AS VARCHAR(4)) + '-04-01' END ORDER BY YEAR_STARTDATE DESC)

SELECT @TOTALMTRS = SUM(SALERETURN_DESC.SALRET_MTRS)  FROM  SALERETURN_DESC INNER JOIN SALERETURN ON SALERETURN_DESC.SALRET_NO = SALERETURN.SALRET_NO AND SALERETURN_DESC.SALRET_YEARID = SALERETURN.SALRET_yearid WHERE CAST(SALERETURN.SALRET_DATE AS DATE)  >= DATEADD(day, -7, GETDATE()) AND SALERETURN.SALRET_YEARID = @YEARID
SET @SUBJECT = 'SSMPL-WEEKLY GOODS RETURN INTIMATION FROM ' + CAST(DAY(DATEADD(day, -7, GETDATE())) AS VARCHAR(2)) + '-' + CAST(MONTH( DATEADD(day, -7, GETDATE())) AS VARCHAR(2)) + '-' + CAST(YEAR(DATEADD(day, -7, GETDATE())) AS VARCHAR(4))
DECLARE @BODY VARCHAR(MAX)
SET @BODY = ''


DECLARE @LEDGERNAME AS VARCHAR(100), @PARTYMTRS FLOAT
declare CUR_MAIN cursor for select LEDGERS.Acc_cmpname, SUM(SALERETURN.SALRET_TOTALMTRS) AS PARTYMTRS from SALERETURN INNER JOIN LEDGERS ON LEDGERS.Acc_id = SALERETURN.SALRET_ledgerid  WHERE CAST(SALERETURN.SALRET_DATE AS DATE)  >= DATEADD(day, -7, GETDATE()) AND SALERETURN.SALRET_YEARID = @YEARID GROUP BY LEDGERS.ACC_CMPNAME
open CUR_MAIN
	fetch next from CUR_MAIN into @LEDGERNAME, @PARTYMTRS
	while @@Fetch_STATUS = 0
		begin

			SET @ENTRYBODY = (SELECT  
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='80px', TD= SALERETURN.SALRET_DATE ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= LEDGERS.Acc_cmpname ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='150px', TD= ISNULL(DESIGNMASTER.DESIGN_NO, '') ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=SUM(CAST(SALERETURN_DESC.SALRET_MTRS AS DECIMAL(18,2))) ,'',
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= ISNULL(TRANSLEDGERS.Acc_cmpname, '') ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='150px', TD= ISNULL(SALERETURN.SALRET_LRNO, '') ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= SALERETURN.SALRET_remarks ,''

			FROM  DESIGNMASTER RIGHT OUTER JOIN
									 LEDGERS INNER JOIN
									 SALERETURN_DESC INNER JOIN
									 SALERETURN ON SALERETURN_DESC.SALRET_NO = SALERETURN.SALRET_NO AND SALERETURN_DESC.SALRET_YEARID = SALERETURN.SALRET_yearid ON 
									 LEDGERS.Acc_id = SALERETURN.SALRET_ledgerid LEFT OUTER JOIN
									 LEDGERS AS TRANSLEDGERS ON SALERETURN.SALRET_TRANSID = TRANSLEDGERS.Acc_id ON DESIGNMASTER.DESIGN_id = SALERETURN_DESC.SALRET_DESIGNID
			WHERE LEDGERS.ACC_CMPNAME = @LEDGERNAME AND CAST(SALERETURN.SALRET_DATE AS DATE)  >= DATEADD(day, -7, GETDATE()) AND SALERETURN.SALRET_YEARID = @YEARID
			GROUP BY SALERETURN.SALRET_DATE, LEDGERS.Acc_cmpname, ISNULL(DESIGNMASTER.DESIGN_NO, ''), ISNULL(TRANSLEDGERS.Acc_cmpname, ''), ISNULL(SALERETURN.SALRET_LRNO, ''), 
									 SALERETURN.SALRET_remarks
			ORDER BY SALERETURN.SALRET_DATE
			FOR XML PATH ('tr'))


			SET @BODY = @BODY + N'<H1 style="font-family:Tahoma; font-size:11px;">' + @LEDGERNAME + '</H1>' +
							N'<Table Border = "1">' + 
							N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Date</Th><Th>Name</Th><Th>Design No</Th><Th>Mtrs</Th><Th>Transport Name</Th><Th>LR No</Th><Th>Remarks</Th></Tr>'+
							+ISNULL(@ENTRYBODY,'')+ N'<tfoot><tr><td colspan="3"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">GRAND TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@PARTYMTRS AS VARCHAR) + N'</td>
							</tr></tfoot></Table></html>'


			fetch next from CUR_MAIN into @LEDGERNAME, @PARTYMTRS
		end
close CUR_MAIN
deallocate CUR_MAIN



--GETTING GRANDTOTAL
SET @BODY = @BODY + N'<Table Border = "1">' + 
							 N'<tfoot><tr><td colspan="2"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="lightgreen">GRAND TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="lightgreen">' + CAST(@TOTALMTRS AS VARCHAR) + N'</td>
							</tr></tfoot></Table></html>'


DECLARE @FINALBODY VARCHAR(MAX)
SET @FINALBODY =  @BODY 


EXEC msdb.dbo.sp_send_dbmail
@profile_name='TEXTRADE',
@recipients= 'gulkitjain@gmail.com;',	--infoavisindustries@gmail.com;gm@avisindustries.in;
@subject=@SUBJECT,
@body=@FINALBODY,
@body_format = 'HTML'