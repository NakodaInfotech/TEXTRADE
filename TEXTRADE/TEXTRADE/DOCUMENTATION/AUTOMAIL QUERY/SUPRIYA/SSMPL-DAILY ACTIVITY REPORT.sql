DECLARE @SUBJECT VARCHAR(100), @BODY VARCHAR(MAX), @ENTRYBODY VARCHAR(MAX), @CMPNAME AS VARCHAR(100)
SET @SUBJECT = 'DAILY ACTIVITY REPORT FOR ' + CAST(DAY( GETDATE()) AS VARCHAR(2)) + '-' + CAST(MONTH( GETDATE()) AS VARCHAR(2)) + '-' + CAST(YEAR( GETDATE()) AS VARCHAR(4))
SET @CMPNAME = 'SUPRIYA SILK MILLS PVT. LTD.'

DECLARE @TYPE AS VARCHAR(100), @TOTALENTRIES INT, @TOTALMTRS DECIMAL(18,2), @TOTALAMT DECIMAL(18,2)
declare CUR_MAIN cursor for select DISTINCT DAILYACTIVITY.TYPE, COUNT(DAILYACTIVITY.NOOFENTRIES) AS NOOFENTRIES, SUM(DAILYACTIVITY.TOTALMTRS) AS TOTALMTRS, SUM(DAILYACTIVITY.TOTALAMT) AS TOTALAMT
		from DAILYACTIVITY 
		WHERE DAILYACTIVITY.YEARID = (SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = @CMPNAME and year_startdate= CASE WHEN MONTH(GETDATE()) > 3 THEN CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-04-01' ELSE CAST(YEAR(GETDATE())-1 AS VARCHAR(4)) + '-04-01' END  ORDER BY YEAR_STARTDATE DESC)  AND CAST(DAILYACTIVITY.DATE AS DATE) = CAST(GETDATE() AS DATE)
		GROUP BY DAILYACTIVITY.TYPE
open CUR_MAIN
	fetch next from CUR_MAIN into @TYPE, @TOTALENTRIES,  @TOTALMTRS, @TOTALAMT
	while @@Fetch_STATUS = 0
		begin
			
			SET @ENTRYBODY = (SELECT  DISTINCT
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@width]='250px', TD= DAILYACTIVITY.NAME ,'', 
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=COUNT(CAST(DAILYACTIVITY.NOOFENTRIES AS INT)) ,'',
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=SUM(CAST(DAILYACTIVITY.TOTALMTRS AS DECIMAL(18,2))) ,'',
			[TD/@style]='font-family:Tahoma; font-size:11px;', [TD/@align]='right',[TD/@width]='80px', TD=SUM(CAST(DAILYACTIVITY.TOTALAMT AS DECIMAL(18,2))) ,''

			FROM DAILYACTIVITY
			WHERE DAILYACTIVITY.TYPE = @TYPE AND DAILYACTIVITY.YEARID = (SELECT TOP 1 YEAR_ID FROM YEARMASTER inner join CMPMASTER ON YEAR_CMPID = CMP_ID WHERE CMP_DISPLAYEDNAME = @CMPNAME  and year_startdate= CASE WHEN MONTH(GETDATE()) > 3 THEN CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-04-01' ELSE CAST(YEAR(GETDATE())-1 AS VARCHAR(4)) + '-04-01' END ORDER BY YEAR_STARTDATE DESC) AND CAST(DAILYACTIVITY.DATE AS DATE) = CAST(GETDATE() AS DATE)
			GROUP BY DAILYACTIVITY.NAME
			ORDER BY DAILYACTIVITY.NAME
			FOR XML PATH ('tr'))



			SET @BODY = ISNULL(@BODY,'') + N'<H1 style="font-family:Tahoma; font-size:11px;">' + @TYPE + '</H1>' +
							N'<Table Border = "1">' + 
							N'<Tr style="font-family:Tahoma; font-size:11px;"><Th>Name</Th><Th>Entries</Th><Th>Mtrs</Th><Th>Amount</Th></Tr>'+
							+ISNULL(@ENTRYBODY,'')+ N'<tfoot><tr><td colspan="1"; style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" bgcolor="yellow">TOTAL:</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALENTRIES AS VARCHAR) + N'</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALMTRS AS VARCHAR) + N'</td>
							<td style="font-family:Tahoma; font-size:11px; font-weight:bold; color:black;" align="right" bgcolor="yellow">' + CAST(@TOTALAMT AS VARCHAR) + N'</td>
							</tr></tfoot></Table></html>'
			
		

			fetch next from CUR_MAIN into @TYPE, @TOTALENTRIES,  @TOTALMTRS, @TOTALAMT
		end
close CUR_MAIN
deallocate CUR_MAIN



DECLARE @FINALBODY VARCHAR(MAX)
SET @FINALBODY = @BODY 


EXEC msdb.dbo.sp_send_dbmail
@profile_name='TEXTRADE',
@recipients='ncjain1975@gmail.com;vimaljjain@yahoo.co.in;bhavinpjain@yahoo.com;',
@subject=@SUBJECT,
@body=@FINALBODY,
@body_format = 'HTML'








