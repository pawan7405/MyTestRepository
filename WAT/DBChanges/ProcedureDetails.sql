USE [Prism_Datamart_MVC]
GO

/****** Object:  StoredProcedure [dbo].[proc_Easyjet_GetActivityTracketDataList]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_Easyjet_GetActivityTracketDataList]
GO

/****** Object:  StoredProcedure [dbo].[proc_Admin_ActivityTrackerFlightRefundDataFileUpload]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_Admin_ActivityTrackerFlightRefundDataFileUpload]
GO

/****** Object:  StoredProcedure [dbo].[proc_WAT_EasyJet_ActivityTrackerFlightRefundDataValidate]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_WAT_EasyJet_ActivityTrackerFlightRefundDataValidate]
GO




/****** Object:  StoredProcedure [dbo].[proc_WAT_DataMasterUpdate]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_WAT_DataMasterUpdate]
GO




/****** Object:  StoredProcedure [dbo].[proc_WAT_FetchCaseSectorFromFormatedData]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_WAT_FetchCaseSectorFromFormatedData]
GO

/****** Object:  StoredProcedure [dbo].[proc_WAT_FetchUMIDAvailableStatus]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_WAT_FetchUMIDAvailableStatus]
GO

/****** Object:  StoredProcedure [dbo].[proc_WAT_AddUpdate_UploadDataColumnMapping]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_WAT_AddUpdate_UploadDataColumnMapping]
GO

/****** Object:  StoredProcedure [dbo].[proc_WAT_Fetch_UploadDataColumnMapping]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_WAT_Fetch_UploadDataColumnMapping]
GO




/****** Object:  StoredProcedure [dbo].[proc_WAT_FetchDynamicControlValue]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_WAT_FetchDynamicControlValue]
GO



/****** Object:  StoredProcedure [dbo].[Proc_WAT_LiveTMEasyjetDashbaord]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[Proc_WAT_LiveTMEasyjetDashbaord]
GO

/****** Object:  StoredProcedure [dbo].[proc_WAT_ADDUpdateWorkDetails_ALG]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_WAT_ADDUpdateWorkDetails_ALG]
GO

/****** Object:  StoredProcedure [dbo].[proc_rpt_Easyjet_WAT_ProductivityDetail]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_rpt_Easyjet_WAT_ProductivityDetail]
GO



/****** Object:  StoredProcedure [dbo].[Proc_WAT_UpdateDataMasterDispositionStatus]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[Proc_WAT_UpdateDataMasterDispositionStatus]
GO

/****** Object:  StoredProcedure [dbo].[Proc_WAT_UpdateDataMasterDispositionStatus_Job]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[Proc_WAT_UpdateDataMasterDispositionStatus_Job]
GO


/****** Object:  StoredProcedure [dbo].[proc_WAT_FetchCaseFromDataMasterFIFO]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[proc_WAT_FetchCaseFromDataMasterFIFO]
GO


/****** Object:  StoredProcedure [dbo].[Proc_WAT_LiveTMEasyjetDashbaord_New]    Script Date: 9/27/2021 4:02:16 PM ******/
DROP PROCEDURE [dbo].[Proc_WAT_LiveTMEasyjetDashbaord_New]
GO

/****** Object:  StoredProcedure [dbo].[Proc_WAT_LiveTMEasyjetDashbaord_New]    Script Date: 9/27/2021 4:02:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Mahesh Mamgain>
-- Create date: <2021-09-08>
-- Description:	<Create a dasboard for WAT TM Dashboard for specific Process>
-- =============================================
--exec Proc_WAT_LiveTMEasyjetDashbaord @DateFrom=N'2021-08-01',@DateTo=N'2021-09-24',@ClientIDs=N'YF@~@DRl0Rw0U=',@TMIDs=N'fGmIEm4wxZw=',@AgentIDs=N'Orcp0OzH4rk=',@WorkItemIDs=N'tp3@~@cCcHzyo=',@LoginMID=N'22',@AccessType=N'5'
--exec Proc_WAT_LiveTMEasyjetDashbaord @DateFrom=N'2021-08-01',@DateTo=N'2021-09-23',@ClientIDs=N'YF@~@DRl0Rw0U=',@TMIDs=N'',@AgentIDs='',@WorkItemIDs='', @LoginMID=N'22100',@AccessType=N'9'
--exec Proc_WAT_LiveTMEasyjetDashbaord @DateFrom=N'',@DateTo=N'',@ClientIDs=N'YF@~@DRl0Rw0U=',@TMIDs=N'5186',@AgentIDs=N'5049',@WorkItemIDs=N'1519',@LoginMID=N'22',@AccessType=N'5'
--exec Proc_WAT_LiveTMEasyjetDashbaord @DateFrom=default,@DateTo=default,@ClientIDs=N'',@TMIDs=N'',@AgentIDs=N'',@WorkItemIDs=N'',@LoginMID=N'22',@AccessType=N'5'


CREATE PROCEDURE [dbo].[Proc_WAT_LiveTMEasyjetDashbaord_New]
	-- Add the parameters for the stored procedure here
	  @DateFrom DATETIME = '1900-01-01 00:00:00.000' ,
      @DateTo DATETIME = '1900-01-01 00:00:00.000' ,
	  @ClientIDs VARCHAR(100) = '' ,
	  @TMIDs Varchar(2000)='',
	  @AgentIDs Varchar(2000)='',
	  @WorkItemIDs VARCHAR(100)='',
	  @LoginMID VARCHAR(100) = '' ,
      @AccessType INT = 0
   
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;
		DECLARE @trans varchar(MAX)
		set @trans ='SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
		'  
	    DECLARE @ErrorNumber INT
        DECLARE @ErrorMessage VARCHAR(MAX)
        SET @ErrorNumber = 0 
        SET @ErrorMessage = 'Failed to extract WAT TM Dashbaord dashbaord data following error.. ;'
		
		 -- SET @DateFrom  = '2021-08-20 00:00:00.000' 
		 -- SET @DateTo  = '2021-09-20 00:00:00.000' 
		

		--SET  @ClientIDs  = 'YF@~@DRl0Rw0U=' 		
		SET @ClientIDs = ISNULL(dbo.fn_str_FROM_BASE64(@ClientIDs), '')
		SET @TMIDs=ISNULL(dbo.fn_str_FROM_BASE64(@TMIDs), '')
		SET @AgentIDs = ISNULL(dbo.fn_str_FROM_BASE64(@AgentIDs), '')
		SET @WorkItemIDs = ISNULL(dbo.fn_str_FROM_BASE64(@WorkItemIDs), '')
		
		DECLARE @MainStr VARCHAR(MAX) = ''
		DECLARE @ClientVC VARCHAR(Max) = ''
		DECLARE @CampaignVC VARCHAR(Max) = ''
        DECLARE @TMVC VARCHAR(500) = ''
        DECLARE @AgentVC VARCHAR(500) = ''
        DECLARE @WorkItemVC VARCHAR(500) = ''
	    DECLARE @AccessVC VARCHAR(200) = ''
        DECLARE @DateVC VARCHAR(200) = ''
		Declare @StartD Datetime
		Declare @EndD Datetime

		;WITH ListDates(AllDates) AS
		(    SELECT DATEADD(DAY,0,@DateFrom) AS DATE
			UNION ALL
			SELECT DATEADD(DAY,1,AllDates)
			FROM ListDates 
			WHERE AllDates <= DATEADD(DAY,-1,@DateTo))
		SELECT AllDates
		INTO #AllDates FROM ListDates
		
		--;with hrcte as
		--(
		--	SELECT 0 AS AllHour
		--	UNION ALL
		--	SELECT AllHour + 1
		--	FROM hrcte 
		--	WHERE AllHour + 1 < 24
		--)
		--SELECT hrcte.AllHour INTO #AllTime FROM hrcte		

		--SELECT AllDates,AllHour INTO #AllDateHours FROM #AllDates CROSS JOIN #AllTime
		
		--IF OBJECT_ID(N'dbo.#AllDates', N'U') IS NOT NULL  
		--   DROP TABLE #AllDates;  
		
		--IF OBJECT_ID(N'dbo.#AllTime', N'U') IS NOT NULL  
		--   DROP TABLE #AllTime;  
		IF(@DateFrom = '1900-01-01 00:00:00.000')
		BEGIN
		SET @DateFrom=CONVERT(CHAR(10), Getdate() ,126) + ' 00:00:00.000'
		SET @DateTo=CONVERT(CHAR(10), Getdate() ,126) + '  00:00:00.000'
		END

		set @StartD=cast(@DateFrom as datetime)
		set @EndD=cast(DATEADD(DD, 1,@DateTo) as datetime)
           
		 IF ( @StartD <> '1900-01-01 00:00:00.000' )
                BEGIN 
                  
				  
				  SET @DateVC = ' and  WM.CreatedDateTime BETWEEN '''
                                + CAST(@StartD AS VARCHAR) + ''' AND '''
                                + CAST(@EndD AS VARCHAR) + ''''
                     
                END
            ELSE
                SET @DateVC = ' '

			IF (@ClientIDs <> '')
            BEGIN      
                SET @ClientVC = ' AND  UM.ClientID  IN ('+@ClientIDs+') '        
            END
			ELSE
			BEGIN
			   SET @ClientIDs=(Select DefaultClientID FROM Prism_DataMart_MVC.dbo.LoginMaster WHERE LoginMID = @LoginMID AND FreezeStatus=0)		
			   SET @ClientVC = ' AND  UM.ClientID  IN ('+@ClientIDs+') '		
			END

			--for data detail table
			IF (@ClientIDs <> '')
            BEGIN      
                SET @CampaignVC = ' AND  WD.CampaignID  IN (SELECT DISTINCT CampaignID FROM EmeraldHierarchies_MVC.dbo.Campaigns WHERE ProjectID IN (
										SELECT ProjectID from EmeraldHierarchies_MVC.dbo.Projects WHERE ClientID IN ('+@ClientIDs+') AND IsActive=1) AND IsActive=1) '        
            END
			ELSE
			BEGIN
			   SET @ClientIDs=(Select DefaultClientID FROM Prism_DataMart_MVC.dbo.LoginMaster WHERE LoginMID = @LoginMID AND FreezeStatus=0)		
			    SET @CampaignVC = ' AND  WD.CampaignID  IN (SELECT DISTINCT CampaignID FROM EmeraldHierarchies_MVC.dbo.Campaigns WHERE ProjectID IN (
										SELECT ProjectID from EmeraldHierarchies_MVC.dbo.Projects WHERE ClientID IN ('+@ClientIDs+') AND IsActive=1) AND IsActive=1) '        	
			END

			IF (@TMIDs <> '')
            BEGIN      
                SET @TMVC = ' AND  GU.GlobalUserID  IN ('+@TMIDs+') '        
            END
			ELSE
			BEGIN      
                SET @TMVC = ''        
            END

			IF (@AgentIDs <> '')
            BEGIN      
                SET @AgentVC = ' AND  LM.GlobalUserID  IN ('+@AgentIDs+') '        
            END
			ELSE
			BEGIN      
                SET @AgentVC = ''        
            END

			IF (@WorkItemIDs <> '')
            BEGIN      
                SET @WorkItemVC = ' AND  WD.WorkIMID  IN ('+@WorkItemIDs+') '        
            END
			ELSE
			BEGIN      
                SET @WorkItemVC = ''        
            END

			 

			--- Total Unique Case Uploaded, Case Processed, Case Pending, 
			
		SET @MainStr='
						SELECT WM.DataMTID,WM.UMID,WM.[Date],WM.PNR,WM.DispositionStatus,WM.DispositionDate,
						WM.DispositionBy AS AgentGlobalUserID,
						WM.CreatedDateTime 
						,UM.ClientID, UM.[Name] AS WorkItemQueue				
						INTO #RAWDATA  FROM dbo.WAT_DataMaster WM 
						INNER JOIN uploadMaster UM ON WM.UMID=UM.UMID AND UM.FreezeStatus=0
						JOIN WAT_WorkDivisionWorkItemDetail WD ON WM.UMID=WD.UMID
						LEFT JOIN LoginMaster LM ON WM.DispositionBy=LM.LoginMID and LM.FreezeStatus=0
						LEFT JOIN [EmeraldHierarchies_MVC].dbo.GlobalUserRoles GUR ON GUR.GlobalUserID = LM.GlobalUserID AND GUR.FreezeStatus=0 
						LEFT JOIN [EmeraldHierarchies_MVC].dbo.GlobalUsers GU ON GU.GlobalUserID = GUR.ParentGlobalUserID
						WHERE WM.FreezeStatus=0 '+ @DateVC + @ClientVC + @WorkItemVC +'
						
						SELECT WD.DataDID,WD.WorkGMID,WD.WorkIMID,WD.OutcomeMID,WD.StatusDID,WD.LoginMID,WD.GlobalUserID,WD.CreatedDateTime,
						WD.ActiveWorkStatusDID,WD.DataMTID,LM.EmployeeName AS AgentName,  GU.FirstName + '' '' + GU.LastName AS [TM] , GU.GlobalUserID AS ParentGlobalUserID	
						INTO  #WatDataDetail 
						FROM WAT_DataDetail_ALG WD 
						LEFT JOIN LoginMaster LM ON WD.CreatedBy=LM.LoginMID and LM.FreezeStatus=0
						LEFT JOIN [EmeraldHierarchies_MVC].dbo.GlobalUserRoles GUR ON GUR.GlobalUserID = LM.GlobalUserID AND GUR.FreezeStatus=0 
						LEFT JOIN [EmeraldHierarchies_MVC].dbo.GlobalUsers GU ON GU.GlobalUserID = GUR.ParentGlobalUserID
						WHERE WD.FreezeStatus=0
						' + REPLACE(@DateVC,'WM.','WD.') + @CampaignVC + @TMVC + @AgentVC + @WorkItemVC +'
						

						SELECT WD.StatusDID,WD.LoginMID,WD.ActionSMID,WD.WorkIMID,WD.ActiveWorkStatusDID,WD.ActionStartDateTime,WD.ActionEndDateTime,WD.ElapsedTime,
						WD.DataMTID,DD.AgentName,DD.[TM],DD.ParentGlobalUserID
						INTO #WatStatusDetail  FROM WAT_ActionStatusDetail_ALG WD 
						JOIN #WatDataDetail DD ON WD.DataMTID=DD.DataMTID
						WHERE WD.ActiveWorkStatusDID IS NOT NULL			
						' + REPLACE(@DateVC,'WM.CreatedDateTime','WD.ActionEndDateTime') + @CampaignVC + REPLACE(@TMVC,'GU.GlobalUserID','DD.ParentGlobalUserID') + REPLACE(@AgentVC,'LM.','DD.')
						 + @WorkItemVC +'

						-----	Table 0	   -------
					     SELECT TotalUniqueCaseUploaded=(SELECT Count(*) FROM #RAWDATA)
						,CaseProcessed=(SELECT Count(*) FROM #RAWDATA WHERE DispositionStatus=2 )
						,CasePending=(SELECT Count(*) FROM WAT_DataMaster WHERE DispositionStatus=0 )
						,CaseInProcess=(SELECT Count(*) FROM WAT_DataMaster WHERE DispositionStatus=1 )	
						,ProductiveAux=(SELECT CONVERT(varchar(8), DATEADD(ms, Sum(ElapsedTime)* 1000, 0), 114) AS ProductiveAux FROM #WatStatusDetail WHERE ActionSMID IN (2,7))
														
						------	Table 1- Agent Wise Case Count	-------
						SELECT DISTINCT AgentName, Count(*) AS CaseCount
						FROM #WatDataDetail
						GROUP BY AgentName
						ORDER BY Count(*) DESC
						------	Table 2- TM Wise Case Count	  --------
						SELECT DISTINCT [TM],Count(*)  AS CaseCount
						FROM #WatDataDetail
						GROUP BY [TM]
						ORDER BY Count(*) DESC
						'
	SET @MainStr = @MainStr + 
					'	------	Table 3- Queue Wise Unique Case Uploaded --------
						SELECT WorkItemQueue,Count(*) AS CaseCount FROM #RAWDATA
						GROUP BY WorkItemQueue ORDER BY WorkItemQueue
						------	Table 4- Queue Wise Unique Case Processed -------- Not In Used
						SELECT WorkItemQueue,Count(*) AS CaseCount FROM #RAWDATA WHERE DispositionStatus=2
						GROUP BY WorkItemQueue ORDER BY WorkItemQueue
						------	Table 5- Queue Wise Case Pending --------
						SELECT WorkItemQueue,Count(*) AS CaseCount FROM #RAWDATA WHERE DispositionStatus=0
						GROUP BY WorkItemQueue ORDER BY WorkItemQueue

						SELECT UM.[Name] AS WorkItemQueue, Count(*) AS CaseCount FROM WAT_DataMaster WD INNER JOIN
						uploadMaster UM ON WD.UMID=UM.UMID
						WHERE WD.DispositionStatus=0
						GROUP BY UM.[Name] ORDER BY UM.[Name]

				    	------	Table 6- Agent Wise EPH --------
						SELECT DISTINCT WSD.AgentName, WSD.LoginMID,WSD.WorkIMID,WSD.ActiveWorkStatusDID,WSD.DataMTID,
						Min(WSD.ActionStartDateTime) AS ActionStartDateTime,
						Max(WSD.ActionEndDateTime) AS ActionEndDateTime,
						Sum(WSD.ElapsedTime) AS TotalTime,
						WM.WorkItemName AS QueueName
						INTO #ProductiveTemp  from #WatStatusDetail   WSD
						JOIN WAT_WorkItemMaster WM ON WSD.WorkIMID=WM.WorkIMID
						WHERE ActionSMID IN (2,7)
						GROUP BY  WSD.AgentName,  WSD.LoginMID, WSD.WorkIMID, WSD.ActiveWorkStatusDID, WSD.DataMTID,WM.WorkItemName
					   			   					   
					    select AgentName, LoginMID,QueueName, CONVERT(NVARCHAR, ActionEndDateTime, 106) AS DataDate, 
						Sum(TotalTime) AS TotalTimeInSeconds,
						cast(Sum(TotalTime) / (60.00*60) as decimal(6, 0)) AS TotalHrs,
						COUNT(Distinct DataMTID) AS TotalCaseCount
						from #ProductiveTemp  
						GROUP BY AgentName, LoginMID,  CONVERT(NVARCHAR, ActionEndDateTime, 106),QueueName
						Order by AgentName, CONVERT(NVARCHAR, ActionEndDateTime, 106), QueueName
					
						SELECT DISTINCT Convert(VARCHAR(10), ActionEndDateTime,105) AS DataDate, 
						DATEPART(HOUR, ActionEndDateTime)  AS AHour ,
						Sum(TotalTime) AS TotalTime,
						COUNT(Distinct DataMTID) AS CaseCount
						INTO #DateWiseProductive from #ProductiveTemp
						GROUP BY Convert(VARCHAR(10), ActionEndDateTime,105),	DATEPART(HOUR, ActionEndDateTime)
		
						 SELECT CONVERT(NVARCHAR, ADH.AllDates, 106) AS AllDates, PD.AHour,PD.CaseCount  --PD.TotalTime 
						 INTO #AllDateProductive FROM #AllDates ADH 
						 LEFT JOIN #DateWiseProductive PD ON  Convert(VARCHAR(10), ADH.AllDates,105)=PD.DataDate
						--Queue Wise Unique Case Processed Tabular-----
					select *
						from 
						(
						  select AllDates, AHour, CaseCount
						  from #AllDateProductive  	
						) src
						pivot
						(
						 Sum(CaseCount) 
						  for AHour in ([0],[1], [2], [3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23])
						) piv;
					'

 PRINT ( @MainStr )
 EXEC ( @MainStr )

END

GO



/****** Object:  StoredProcedure [dbo].[proc_WAT_FetchCaseFromDataMasterFIFO]    Script Date: 9/27/2021 4:02:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

      
  -- =============================================    
-- Author:  <Mahesh Mamgain>    
-- Create date: <23-August-21>    
-- =============================================    
    
--EXEC [proc_WAT_FetchCaseFromDataMasterFIFO] @WorkIMID='1519',@LoginMID='22'    
CREATE PROCEDURE [dbo].[proc_WAT_FetchCaseFromDataMasterFIFO]      
   @WorkIMID INT=0, 
   @StatusDID BIGINT,     
   @LoginMID INT   
AS      
    BEGIN      
       DECLARE @SaveDateTime DATETIME = GETDATE();      
    DECLARE @UMID INT=0    
    DECLARE @DataMTID BIGINT=0    
	DECLARE @ExistDataMTID BIGINT=0  
    DECLARE @InsertStr VARCHAR(MAX)=''    
    BEGIN TRY         
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
    
	IF((SELECT Count(*) FROM WAT_ActionStatusDetail_ALG WHERE StatusDID=@StatusDID AND DataMTID IS NOT NULL)>0)
	BEGIN
		SELECT @ExistDataMTID=DataMTID FROM WAT_ActionStatusDetail_ALG WHERE StatusDID=@StatusDID
		IF((SELECT Count(*) from WAT_DataDetail_ALG where DataMTID = @ExistDataMTID)>0)
			BEGIN
			SET @DataMTID=0
			END
		ELSE
			BEGIN
			SET @DataMTID=@ExistDataMTID
			END
	END

  SELECT @UMID=UMID FROM WAT_WorkDivisionWorkItemDetail  WHERE WorkIMID=@WorkIMID AND FreezeStatus=0    
    
	IF(@DataMTID=0)
	BEGIN
		  SELECT TOP 1 @DataMTID=DataMTID FROM WAT_DataMaster WHERE [Date] IN     
		  (SELECT MIN([Date]) FROM WAT_DataMaster  WHERE FreezeStatus=0 AND DispositionStatus=0 AND UMID=@UMID )    
		  AND FreezeStatus=0 AND DispositionStatus=0 AND UMID=@UMID    
		  ORDER BY DataMTID,ID ASC    
      
		  IF((SELECT Count(*) from WAT_DataMaster WHERE DataMTID=@DataMTID AND DispositionStatus=0)>0)    
		  BEGIN    
		  UPDATE WAT_DataMaster  SET DispositionStatus=1, DispositionDate=GETDATE(), DispositionBy=@LoginMID WHERE DataMTID=@DataMTID    

		  END    
		  ELSE    
		  BEGIN    
		  SELECT TOP 1 @DataMTID=DataMTID FROM WAT_DataMaster WHERE [Date] IN     
		  (SELECT MIN([Date]) FROM WAT_DataMaster WHERE FreezeStatus=0 AND DispositionStatus=0 AND UMID=@UMID )    
		  AND FreezeStatus=0 AND DispositionStatus=0 AND UMID=@UMID    
		  ORDER BY DataMTID,ID ASC    
      
		  UPDATE WAT_DataMaster  SET DispositionStatus=1, DispositionDate=GETDATE(), DispositionBy=@LoginMID WHERE DataMTID=@DataMTID    
		  END    
   
	END

  SELECT LabelNameOnATScreen,CASE WHEN DataMDBColumnName='DATE' THEN 'DataDate' ELSE DataMDBColumnName END AS DataColumn     
		  FROM UploadDataColumnMapping WHERE IsVisibleLabelOnATScreen=1 AND UMID=@UMID   

  SELECT *,CONVERT(VARCHAR(12), [Date], 106) AS DataDate FROM WAT_DataMaster WHERE DataMTID=@DataMTID    
    
  SELECT SectorsVisibility FROM WAT_WorkDivisionWorkItemDetail   WHERE WorkIMID=@WorkIMID AND FreezeStatus=0    
 
  UPDATE WAT_ActionStatusDetail_ALG SET DataMTID =@DataMTID WHERE StatusDID=@StatusDID  
    
  END TRY      
        BEGIN CATCH      
  ROLLBACK TRAN    
         SELECT   ERROR_MESSAGE()    
        END CATCH;                          
    END;      
    
GO



/****** Object:  StoredProcedure [dbo].[Proc_WAT_UpdateDataMasterDispositionStatus_Job]    Script Date: 9/27/2021 4:02:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Mahesh Mamgain>
-- Create date: <2021-09-09>
-- Description:	<Update Disposition status after case dispossed on WRAP>
-- =============================================
--EXEC [Proc_WAT_UpdateDataMasterDispositionStatus_Job] 
CREATE PROCEDURE [dbo].[Proc_WAT_UpdateDataMasterDispositionStatus_Job]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	    SET NOCOUNT ON;
		
		
		SELECT DISTINCT DataMTID INTO #tempDataMTID FROM WAT_DataMaster WHERE DispositionStatus=1 AND  DATEDIFF(Hour, DispositionDate,GETDATE())>2
		AND DataMTID NOT IN (SELECT DataMTID FROM WAT_DataDetail_ALG  WHERE FreezeStatus=0 AND DataMTID IS NOT NULL)
			
		UPDATE  DM SET DM.DispositionStatus=0 , DM.DispositionBy=NULL,DM.DispositionDate=NULL
		FROM  WAT_DataMaster DM 
		INNER JOIN #tempDataMTID DD ON DM.DataMTID=DD.DataMTID
		
		UPDATE WAT_ActionStatusDetail_ALG SET ActionSMID=21, WorkIMID=0,ActiveWorkStatusDID=NULL,ActiveWorkStatus=NULL,ActionEndDateTime=NULL,ElapsedTime=NULL
		WHERE DataMTID IN (SELECT DataMTID FROM #tempDataMTID)

END

GO

/****** Object:  StoredProcedure [dbo].[Proc_WAT_UpdateDataMasterDispositionStatus]    Script Date: 9/27/2021 4:02:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Mahesh Mamgain>
-- Create date: <2021-09-09>
-- Description:	<Update Disposition status after case dispossed on WRAP>
-- =============================================
--exec [Proc_WAT_UpdateDataMasterDispositionStatus] @DataMTID=N'0'
CREATE PROCEDURE [dbo].[Proc_WAT_UpdateDataMasterDispositionStatus]
	  @DataMTID BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	    SET NOCOUNT ON;
		UPDATE WAT_DataMaster SET DispositionStatus=2 WHERE DataMTID=@DataMTID

END

GO


/****** Object:  StoredProcedure [dbo].[proc_rpt_Easyjet_WAT_ProductivityDetail]    Script Date: 9/27/2021 4:02:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*
exec proc_rpt_Easyjet_WAT_ProductivityDetail @StartDate=N'2021-09-01',@EndDate=N'2021-09-02',@ProjectIDs=N'',@CampaignIDs=N'',@TMIDs=N'',@AgentIDs=N'',@WorkGMIDs=N'x9I9ICuEgK0=',@WorkDMIDs=N'',@WorkIMIDs=N'',@LoginMID=N'I0tKv8eg0gU='
Invalid column name 'SubQueue'.
exec [proc_rpt_Easyjet_WAT_ProductivityDetail] @StartDate=N'2020-07-01',@EndDate=N'2020-07-04',@ProjectIDs=N'',@CampaignIDs=N'',@TMIDs=N'',@AgentIDs=N'',@LoginMID=N'usC1YPVLIPs='
*/
CREATE PROCEDURE [dbo].[proc_rpt_Easyjet_WAT_ProductivityDetail]
    (
      @StartDate VARCHAR(50) = '' ,
      @EndDate VARCHAR(50) = '' ,
      @ProjectIDs VARCHAR(1000) = '' ,
      @CampaignIDs VARCHAR(1000) = '' ,
      @TMIDs VARCHAR(1000) = '' ,
      @AgentIDs VARCHAR(1000) = '' ,
      @WorkGMIDs VARCHAR(1000) = '' ,
      @WorkDMIDs VARCHAR(1000) = '' ,
      @WorkIMIDs VARCHAR(1000) = '' ,
      @LoginMID VARCHAR(50)
    )
AS
    BEGIN

        DECLARE @ErrorNumber INT;  
        DECLARE @ErrorMessage VARCHAR(MAX);  
        DECLARE @MainStr VARCHAR(MAX);
        DECLARE @DateVC VARCHAR(200);
        DECLARE @DateAWDVC VARCHAR(200);  
        DECLARE @ProjectVC VARCHAR(500);  
        DECLARE @CampaignVC VARCHAR(500);  
        DECLARE @TMVC VARCHAR(500);  
        DECLARE @AccessVC VARCHAR(500);  
        DECLARE @AgentVC VARCHAR(5000);  
        DECLARE @WorkGroupVC VARCHAR(5000);  
        DECLARE @WorkDivisionVC VARCHAR(5000);  
        DECLARE @WorkItemVC VARCHAR(5000);  
        DECLARE @AccessType INT;
        DECLARE @GlobalUserID VARCHAR(50);
        DECLARE @SDate DATETIME = GETDATE();
        DECLARE @EDate DATETIME = GETDATE();
        DECLARE @EncrypStatus VARCHAR(10); 
        
        SET @ErrorNumber = 0;   
        SET @ErrorMessage = 'Failed to extract Attrition report data following error.. ;';  
  
        BEGIN TRY
            --SET @ClientIDs = dbo.fn_str_FROM_BASE64(@ClientIDs)  usC1YPVLIPs=
			-- select  dbo.fn_str_FROM_BASE64('5QuwFzMoTmk=')
            SET @ProjectIDs = dbo.fn_str_FROM_BASE64(@ProjectIDs);
            SET @CampaignIDs = dbo.fn_str_FROM_BASE64(@CampaignIDs);
            SET @TMIDs = dbo.fn_str_FROM_BASE64(@TMIDs);
            SET @AgentIDs = dbo.fn_str_FROM_BASE64(@AgentIDs);
            SET @WorkGMIDs = dbo.fn_str_FROM_BASE64(@WorkGMIDs);
            SET @WorkDMIDs = dbo.fn_str_FROM_BASE64(@WorkDMIDs);
            SET @WorkIMIDs = dbo.fn_str_FROM_BASE64(@WorkIMIDs);
            SET @LoginMID = dbo.fn_str_FROM_BASE64(@LoginMID);  
            

            SELECT  @EncrypStatus = ISNULL(w.EncryptionType, 0)
            FROM    dbo.WAT_WorkGroupMaster w
            WHERE   w.WorkGMID = @WorkGMIDs;

            --SET @WorkGMIDs = 1035;
            DECLARE @NewColumnName VARCHAR(MAX);
            DECLARE @ActualColumnName VARCHAR(MAX);
            DECLARE @LabelNameList VARCHAR(MAX);
            --SELECT  @NewColumnName = 
			--COALESCE(@NewColumnName + ', ', ',')
   --                 + CAST('[' + d.LabelName + ']=dbo.fn_AESDec(DD.'
   --                 + d.DBColumnName + ',' + @EncrypStatus + ')' AS VARCHAR(200)) ,
   SELECT  @NewColumnName =   COALESCE(@NewColumnName + ', ', ',') + CAST('[' + d.LabelName + ']= (DD.' + d.DBColumnName +')' AS VARCHAR(200)),
                    @ActualColumnName = COALESCE(@ActualColumnName + ', ', '')
                    + CAST('DD.' + d.DBColumnName AS VARCHAR(200)) ,
                    @LabelNameList = COALESCE(@LabelNameList + ',', ',') + '['
                    + CAST(d.LabelName AS VARCHAR(200)) + ']'
            FROM    dbo.WAT_DynamicOutcomeControlMasterDetail d
                    INNER JOIN dbo.WAT_DynamicOutcomeControlMaster m ON d.DOCMID = m.DOCMID
            WHERE   d.FreezeStatus = 0
                    AND m.WorkGMID = @WorkGMIDs
            ORDER BY d.OrderID ASC;


            SET @NewColumnName = STUFF(@NewColumnName, 1, 1, ''); 
            SET @ActualColumnName = STUFF(@ActualColumnName, 1, 1, '');  
            SET @LabelNameList = STUFF(@LabelNameList, 1, 1, '');  

            SET @GlobalUserID = ( SELECT    GlobalUserID
                                  FROM      dbo.LoginMaster
                                  WHERE     LoginMID = @LoginMID
                                );
            IF ( ISDATE(@StartDate) = 0
   OR ISDATE(@EndDate) = 0
               )
                BEGIN
                    SELECT  ERROR_NUMBER() AS ErrorNumber ,
                            'Problem performing operation, Please try later' AS ErrorMessage ,
                            ERROR_MESSAGE() Emsg;
                    RETURN;
                END;
            ELSE
                BEGIN
                    IF ( @StartDate <> ''
                         AND @EndDate <> ''
                       )
                        BEGIN
                            SET @SDate = @StartDate;
                            SET @EDate = @EndDate;
                        END;
                END;

            SET @EDate = CONVERT(VARCHAR(10), @EDate, 121) + ' 23:59:59.000'; 
            SET @DateVC = ' AND  ASD.ActionStartDateTime BETWEEN '''
                + CONVERT(VARCHAR(10), @SDate, 121) + ''' AND '''
                + CONVERT(VARCHAR(50), @EDate, 121) + ''''; 
            SET @DateAWDVC = ' AND  ActivityDate BETWEEN '''
                + CONVERT(VARCHAR(10), @SDate, 121) + ''' AND '''
                + CONVERT(VARCHAR(50), @EDate, 121) + '''';  


            IF EXISTS ( SELECT  *
                        FROM    dbo.LoginMaster
                        WHERE   LoginMID = @LoginMID
                                AND FreezeStatus = 0 )
                BEGIN
                    SET @AccessType = ( SELECT TOP 1
                                                AccessLMID
                                        FROM    dbo.LoginMaster
                                        WHERE   LoginMID = @LoginMID
                                                AND FreezeStatus = 0
                                      );
                END;
            ELSE
                BEGIN
                    SELECT  ERROR_NUMBER() AS ErrorNumber ,
                            'Problem performing operation, Please try later' AS ErrorMessage ,
                            ERROR_MESSAGE() Emsg;
                    RETURN;
                END;

            --IF ( @ClientIDs <> '' )
            --    SET @ClientVC = ' AND  CL.ClientID IN (' + @ClientIDs + ') '  
            --ELSE
            --    SET @ClientVC = ''

            IF ( @ProjectIDs <> '' )
                SET @ProjectVC = ' AND  P.ProjectID IN (' + @ProjectIDs + ') ';  
            ELSE
                SET @ProjectVC = '';

            IF ( @CampaignIDs <> '' )
                SET @CampaignVC = ' AND  C.CampaignID IN (' + @CampaignIDs
                    + ') ';  
            ELSE
                SET @CampaignVC = '';

            IF ( @TMIDs <> '' )
                SET @TMVC = ' AND  GU.GlobalUserID IN (' + @TMIDs + ') ';  
            ELSE
                SET @TMVC = '';

            IF ( @AgentIDs <> '' )
                SET @AgentVC = ' AND  LM.GlobalUserID IN (' + @AgentIDs + ') ';  
            ELSE
                SET @AgentVC = '';

            IF ( @WorkGMIDs <> '' )
                SET @WorkGroupVC = ' AND  ASD.WorkGMID IN (' + @WorkGMIDs
                    + ') ';  
            ELSE
                SET @WorkGroupVC = '';

            IF ( @WorkDMIDs <> '' )
                SET @WorkDivisionVC = ' AND  ASD.WorkDMID IN (' + @WorkDMIDs
                    + ') ';  
            ELSE
                SET @WorkDivisionVC = '';

            IF ( @WorkIMIDs <> '' )
                SET @WorkItemVC = ' AND  ASD.WorkIMID IN (' + @WorkIMIDs
                    + ') ';  
            ELSE
                SET @WorkItemVC = '';

            IF @AccessType NOT IN ( 1, 5 )
                SET @AccessVC = ' AND CL.ClientID IN(SELECT  ClientID
														FROM    dbo.ClientLoginMappingDetail
														WHERE   LoginMID = '
                    + @LoginMID
                    + '
																AND STATUS = 0)';   
            ELSE
                IF @AccessType = 1
                    SET @AccessVC = ' AND  GU.GlobalUserID = ' + @GlobalUserID;
    ELSE
                    SET @AccessVC = '';
					
            SET @MainStr = 'SELECT  ASD.StatusDID ,
									ASD.LoginMID ,
									ASD.ActionSMID ,
									ASD.CampaignID ,
									ASD.WorkGMID ,
									ASD.WorkDMID ,
									ASD.WorkIMID ,
									ASD.ActiveWorkStatus ,
									ASD.ActiveWorkStatusDID ,
									ASD.ActionStartDateTime ,
									ASD.ActionEndDateTime ,
									ASD.ElapsedTime
							INTO    #TempActionStatus
							FROM    dbo.WAT_ActionStatusDetail_ALG ASD WITH (NOLOCK)
							WHERE   ActiveWorkStatusDID IS NOT NULL AND ActiveWorkStatus=0
							
							' + @DateVC + @WorkGroupVC
                + '
							select LoginMID,ActionSMID,CampaignID,WorkGMID,WorkDMID,WorkIMID,ActiveWorkStatusDID,
							MIN(ActionStartDateTime) AS ActionStartDateTime,
							MAX(ActionEndDateTime) AS  ActionEndDateTime,
							SUM(ElapsedTime) AS ElapsedTime
							INTO #DistictStates FROM #TempActionStatus
							 GROUP BY 
							 LoginMID,ActionSMID,CampaignID,WorkGMID,WorkDMID,WorkIMID,ActiveWorkStatusDID
							 ORDER BY ActiveWorkStatusDID

							select LoginMID,CampaignID,WorkGMID,WorkDMID,WorkIMID,ActiveWorkStatusDID,
							MIN(ActionStartDateTime) AS ActionStartDateTime,
							MAX(ActionEndDateTime) AS  ActionEndDateTime,
							SUM(ElapsedTime) AS TotalTransactionTime
							INTO #TransactionState   FROM #DistictStates
							 GROUP BY 
							 LoginMID,CampaignID,WorkGMID,WorkDMID,WorkIMID,ActiveWorkStatusDID
							 ORDER BY ActiveWorkStatusDID

							;SELECT ASD.ActiveWorkStatusDID, DD.ActionStartDateTime AS [FSActionStartDateTime] ,
									DD.ActionEndDateTime AS [FSActionEndDateTime] ,'
                + @NewColumnName
                + '
							INTO    #TempDataDetail
							FROM    #TransactionState ASD
							JOIN dbo.WAT_DataDetail_ALG DD WITH (NOLOCK) ON  DD.ActiveWorkStatusDID = ASD.ActiveWorkStatusDID AND DD.LoginMID=ASD.LoginMID
							WHERE  1=1 
							 ' + @DateVC + @WorkGroupVC;

            SET @MainStr = @MainStr
                + ';

				 select LoginMID,CampaignID,WorkGMID,WorkDMID,WorkIMID,ActiveWorkStatusDID,
							MIN(ActionStartDateTime) AS ActiveStartDateTime,
							MAX(ActionEndDateTime) AS  ActiveEndDateTime,
							SUM(ElapsedTime) AS TotalActiveTime
							INTO #ActiveState  FROM #DistictStates WHERE ActionSMID=2
							 GROUP BY 
							 LoginMID,CampaignID,WorkGMID,WorkDMID,WorkIMID,ActiveWorkStatusDID
							 ORDER BY ActiveWorkStatusDID

							select LoginMID,CampaignID,WorkGMID,WorkDMID,WorkIMID,ActiveWorkStatusDID,
							MIN(ActionStartDateTime) AS WrapStartDateTime,
							MAX(ActionEndDateTime) AS  WrapEndDateTime,
							SUM(ElapsedTime) AS TotalWrapTime
							INTO #WrapState FROM #DistictStates WHERE ActionSMID=7
							 GROUP BY 
							 LoginMID,CampaignID,WorkGMID,WorkDMID,WorkIMID,ActiveWorkStatusDID
							 ORDER BY ActiveWorkStatusDID

							select LoginMID,CampaignID,WorkGMID,WorkDMID,WorkIMID,ActiveWorkStatusDID,
							MIN(ActionStartDateTime) AS AuxStartDateTime,
							MAX(ActionEndDateTime) AS  AuxEndDateTime,
							SUM(ElapsedTime) AS TotalAuxTime
							INTO #AuxState  
							FROM #DistictStates WHERE ActionSMID NOT IN (2,7)
							 GROUP BY 
							 LoginMID,CampaignID,WorkGMID,WorkDMID,WorkIMID,ActiveWorkStatusDID
							 ORDER BY ActiveWorkStatusDID

							 SELECT TS.LoginMID,TS.CampaignID,TS.WorkGMID,TS.WorkDMID,TS.WorkIMID,TS.ActiveWorkStatusDID,
							 TS.ActionStartDateTime,TS.ActionEndDateTime ,ACS.ActiveStartDateTime,ACS.ActiveEndDateTime,
							 WS.WrapStartDateTime,WS.WrapEndDateTime,TS.TotalTransactionTime, ACS.TotalActiveTime, WS.TotalWrapTime,
							  AX.AuxStartDateTime,AX.AuxEndDateTime,AX.TotalAuxTime
							INTO #FinalActivityTime FROM #TransactionState TS
							 LEFT JOIN #ActiveState ACS ON TS.ActiveWorkStatusDID=ACS.ActiveWorkStatusDID
							 LEFT JOIN #WrapState WS ON TS.ActiveWorkStatusDID=WS.ActiveWorkStatusDID	
							 LEFT JOIN #AuxState AX ON TS.ActiveWorkStatusDID=AX.ActiveWorkStatusDID

				SELECT  CONVERT(VARCHAR(10), ASD.ActionStartDateTime, 121) AS [Date] ,
									ASD.LoginMID ,
									LM.EmployeeName AS [AgentName] ,
									LM.EmployeeID,
									GU.FirstName + '' '' + GU.LastName AS [TM] ,
									ASD.CampaignID ,
                                    ASD.WorkGMID ,
                                    ASD.WorkDMID ,
                                    ASD.WorkIMID ,
                                    WGM.WorkGroupName ,
									WDM.WorkDivisionName ,
									WIM.WorkItemName ,
									ASD.ActiveWorkStatusDID ,
									ASD.ActionStartDateTime AS TransactionStartDateTime,ASD.ActionEndDateTime AS TransactionEndDateTime,
									ASD.ActiveStartDateTime,ASD.ActiveEndDateTime,
							        ASD.WrapStartDateTime,ASD.WrapEndDateTime,ASD.TotalTransactionTime, ASD.TotalActiveTime, ASD.TotalWrapTime,
								    ASD.TotalAuxTime	
							INTO    #TempActivityDetails
							FROM    #FinalActivityTime ASD
									JOIN dbo.LoginMaster LM ON LM.LoginMID = ASD.LoginMID
									JOIN [EmeraldHierarchies_MVC].dbo.GlobalUserRoles GUR ON GUR.GlobalUserID = LM.GlobalUserID
																							AND GUR.FreezeStatus=0 
									JOIN [EmeraldHierarchies_MVC].dbo.GlobalUsers GU ON GU.GlobalUserID = GUR.ParentGlobalUserID
									JOIN dbo.WAT_WorkGroupMaster WGM ON WGM.WorkGMID = ASD.WorkGMID
									JOIN dbo.WAT_WorkDivisionMaster WDM ON WDM.WorkDMID = ASD.WorkDMID
									JOIN dbo.WAT_WorkItemMaster WIM ON WIM.WorkIMID = ASD.WorkIMID
							WHERE   1 = 1' + @ProjectVC + @CampaignVC + @TMVC
                + @AgentVC + @WorkGroupVC + @WorkDivisionVC + @WorkItemVC
               + '
							
										update #TempDataDetail set FSActionStartDateTime=null where cast(FSActionStartDateTime as date)=''1900-01-01''
						';

            
            SET @MainStr = @MainStr
                + ';
				
				SELECT  [Date] ,
        [AgentName] ,
		EmployeeID,
        [TM] ,
		--[Campaign],
		[WorkGroup],
		[WorkDivision],
		[WorkItem],
         ColHeader ,
        details ,
        [TransactionStartDateTime],
		[TransactionEndDateTime],
		[ActiveStartDateTime],
		[ActiveEndDateTime],
		[WrapStartDateTime],
		[WrapEndDateTime]	,
		TotalTransactionTime,
		TotalActiveTime, 
		TotalWrapTime,
		ISNULL(TotalAuxTime,0) AS TotalAuxTime,
		CAST(ActiveWorkStatusDID AS VARCHAR(10)) AS ActiveWorkStatusDID
		from
		(
				SELECT  TAD.[Date] ,
												 [AgentName] ,
												 EmployeeID,
												 [TM] ,
												 TAD.WorkGroupName AS [WorkGroup] ,
												 TAD.WorkDivisionName AS [WorkDivision] ,
												 TAD.WorkItemName AS [WorkItem] ,
												TAD.[TransactionStartDateTime],
												TAD.[TransactionEndDateTime],
												TAD.[ActiveStartDateTime],
												TAD.[ActiveEndDateTime],
												TAD.[WrapStartDateTime],
												TAD.[WrapEndDateTime]	,
												TAD.TotalTransactionTime,
												TAD.TotalActiveTime, 
												TAD.TotalWrapTime,	
												TAD.TotalAuxTime,
												TAD.ActiveWorkStatusDID,
												 ' + @LabelNameList
                + '	FROM    #TempActivityDetails TAD
												JOIN #TempDataDetail TD ON TD.ActiveWorkStatusDID = TAD.ActiveWorkStatusDID
												) det
												UNPIVOT
( details FOR ColHeader IN ( ' + @LabelNameList + ' ) ) AS UnPvt
										
									';	

            PRINT ( @MainStr );
            EXEC (@MainSTR);
        END TRY  
        BEGIN CATCH  	
            SELECT  ERROR_NUMBER() AS ErrorNumber ,
                    'Problem performing operation,Please try later' AS ErrorMessage ,
                    ERROR_MESSAGE() Emsg;
		 -- SELECT * FROM tblcolumn
        END CATCH;
    END;
GO

/****** Object:  StoredProcedure [dbo].[proc_WAT_ADDUpdateWorkDetails_ALG]    Script Date: 9/27/2021 4:02:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  
DECLARE @p16 BIGINT  
SET @p16 = 1  
EXEC proc_WAT_ADDUpdateWorkDetails @DataDID = 0, @WorkGMID = 1,  
    @CampaignID = 2365, @WorkDMID = 1, @WorkIMID = 1, @DataValue = 10,  
    @RefNoStatus = 1, @RefNo = N'Test Ref', @OutcomeMID = 1, @LoginMID = 499,  
    @GlobalUserID = N'56890', @StatusDID = N'19', @HostName = DEFAULT,  
    @ActiveWorkStatusDID = 4, @AgentWorkDID = 4,@ActiveWorkStatus = 0,  
    @ErrorNumber = @p16 OUTPUT  
SELECT  @p16  
*/  
CREATE PROCEDURE [dbo].[proc_WAT_ADDUpdateWorkDetails_ALG]  
    (  
      @DataDID BIGINT = 0 ,  
      @WorkGMID INT = 0 ,  
      @CampaignID BIGINT = 0 ,  
      @WorkDMID INT = 0 ,  
      @WorkIMID INT = 0 ,  
      @StatusDID BIGINT = 0 ,  
      @LoginMID INT = 0 ,  
      @GlobalUserID VARCHAR(50) = '' ,  
      @HostName VARCHAR(50) = '' ,  
      @ActiveWorkStatusDID BIGINT = 0 ,  
      @AgentWorkDID BIGINT = 0 ,  
      @ActiveWorkStatus TINYINT = 0 ,  
      @DynamicControls VARCHAR(MAX) = '' ,  
      @StartDateTime VARCHAR(50) = '1900-01-01 00:00:00.000' ,  
	  @DataMTID BIGINT=NULL,
      @ErrorNumber BIGINT OUTPUT  
    )  
AS  
    BEGIN  
        SET @ErrorNumber = 0;   
        DECLARE @SaveDateTime VARCHAR(25);  
        DECLARE @ptrHandle INT;   
        DECLARE @strMainData VARCHAR(MAX);   
        DECLARE @QueryStr VARCHAR(MAX) = '';   
		DECLARE @UMID INT=0;
		
		IF(@DataMTID=0)
			BEGIN
			SET @DataMTID=NULL
			END
		ELSE
			BEGIN
			IF((SELECT Count(*)  FROM WAT_DataMaster WHERE DataMTID=@DataMTID)>0)
					BEGIN
					SET @UMID=(SELECT UMID FROM WAT_DataMaster WHERE DataMTID=@DataMTID)
					END
			END
		
	
		DECLARE @CatID INT=0; 
		DECLARE @Assignment BIT=0;

		DECLARE @DataALGID INT=0
		DECLARE @ClientMID INT=0
        BEGIN TRY  
		
		DECLARE @RedirectMID INT;         
        SET @RedirectMID = ( SELECT dbo.fn_WATGetDetail(@LoginMID));	

		SELECT @CatID=Category,@Assignment=Assignment ,@ClientMID=ClientMID
		FROM  WAT_ActivityTrackerScreen 
		WHERE RedirectMID=@RedirectMID AND Category=1---- RedirectURL='/WAT/ActivityTracker_ALG/'  		 


            SET @SaveDateTime = CONVERT(VARCHAR(25),GETDATE(),121);  
            EXEC sp_xml_preparedocument @ptrHandle OUTPUT, @DynamicControls;  
  
            IF OBJECT_ID('tempdb..#tmpScriptData') IS NOT NULL  
                DROP TABLE #tmpScriptData;    
    
            SELECT  *  
            INTO    #tmpScriptData  
            FROM    OPENXML(@ptrHandle, '/NewDataSet/Table', 2)          
       WITH (    
         ParaName VARCHAR(200) ,    
         Value VARCHAR(500) ,    
         ID VARCHAR(50)    
        );    
        --DECLARE @strMainData VARCHAR(MAX)   
  
            SELECT  @strMainData = COALESCE(@strMainData + ',', '')  
                    + CAST(a.ParaName AS VARCHAR)  
            FROM    ( SELECT DISTINCT  
                                ParaName  
                      FROM      #tmpScriptData  
                    ) a;   
  
    
            IF ( @StartDateTime = '' )  
                SET @StartDateTime = '1900-01-01';  
  
            IF ( @DataDID = 0 )  
                BEGIN  
                    IF ( ( SELECT   ActionEndDateTime  
                           FROM     dbo.WAT_ActionStatusDetail_ALG  
                           WHERE    StatusDID = @StatusDID  
                         ) IS NULL )  
                        BEGIN  

							--DECLARE @WorkFinishedHour INT=0

							--SELECT @WorkFinishedHour =DATEDIFF(HOUR,ActionStartDateTime ,@SaveDateTime) 
							--FROM WAT_ActionStatusDetail_ALG 
							--WHERE   StatusDID = @StatusDID;  

							--IF (@WorkFinishedHour >12)
							--BEGIN
							--    SELECT   @SaveDateTime =DATEADD(hour,8,ActionStartDateTime) 
							--	FROM WAT_ActionStatusDetail_ALG WHERE StatusDID = @StatusDID;  
							--END
							--ELSE IF (@WorkFinishedHour >16)
							--BEGIN							 
							--	SELECT  @SaveDateTime =ActionStartDateTime 
							--	FROM WAT_ActionStatusDetail_ALG WHERE StatusDID = @StatusDID;  
							--END

                            UPDATE  dbo.WAT_ActionStatusDetail_ALG  
                            SET     ActionEndDateTime = @SaveDateTime ,  
                                    ElapsedTime = DATEDIFF(SECOND,  
                                                           ActionStartDateTime,  
                                                           @SaveDateTime)  
                            WHERE   StatusDID = @StatusDID;  
                        END;  
                
				PRINT('@DataMTID '+ CAST(@DataMTID AS VARCHAR));
		PRINT('@UMID '+ CAST(@UMID AS VARCHAR));

                    IF ( ( SELECT   COUNT(*)  
                           FROM     dbo.WAT_ActionStatusDetail_ALG  
                           WHERE    ActiveWorkStatusDID = @ActiveWorkStatusDID  
                                    AND ActiveWorkStatus = 1  
                         ) = 0 )  
  
                        BEGIN  
    	
                            INSERT  INTO dbo.WAT_ActionStatusDetail_ALG  
                                    ( LoginMID ,  
                                      ActionSMID ,  
                                      WorkGMID ,  
                                      CampaignID ,  
                                      WorkDMID ,  
                                      WorkIMID ,  
                                      ActiveWorkStatusDID ,  
									  ActiveWorkStatus ,  
                                      ActionStartDateTime ,  
                                      ActionEndDateTime ,  
									  ActionComments ,  
                                      ElapsedTime , 
									  DataMTID
                                    )  
                                    SELECT  LoginMID ,  
                                            2 ,  
                                            WorkGMID ,  
                                            CampaignID ,  
                                            WorkDMID ,  
                                            WorkIMID ,  
                                            @ActiveWorkStatusDID ,  
                                            1 ,  
                                            @SaveDateTime ,  
                                            @SaveDateTime ,  
                                            ActionComments ,  
                                            ISNULL(( SELECT SUM(ElapsedTime)  
                                                     FROM   WAT_ActionStatusDetail  
                                                     WHERE  ActiveWorkStatusDID = @ActiveWorkStatusDID  
                                                            AND ActionSMID = 2  
                                                            AND ISNULL(ActiveWorkStatus,  
                                                              0) = 0  
                                                   ), 0) ,
										    @DataMTID 
                                    FROM    dbo.WAT_ActionStatusDetail_ALG  
                                    WHERE   StatusDID = @ActiveWorkStatusDID;  
                            PRINT 'END'     
                            --SET @ErrorNumber = @@IDENTITY; 
							SET @ErrorNumber = SCOPE_IDENTITY();  
       PRINT ('start1' ) 
                        
                            INSERT  INTO dbo.WAT_ActionStatusDetail_ALG  
                                    ( LoginMID ,  
                                      ActionSMID ,  
                                      WorkGMID ,  
                                      CampaignID ,  
                                      WorkDMID ,  
                                      WorkIMID ,  
                                      ActionStartDateTime ,  
                                      ActionEndDateTime ,  
                                      ActionComments   
                                    )  
                            VALUES  ( @LoginMID , -- LoginMID - bigint  
                                      21 , -- ActionSMID - tinyint  
                                      @WorkGMID , -- WorkGMID - int  
                                      @CampaignID , -- CampaignID - bigint  
                                      @WorkDMID , -- WorkDMID - int  
                                      0 , -- WorkIMID - int  
                                      @SaveDateTime , -- ActionStartDateTime - datetime  
                                      NULL , -- ActionEndDateTime - datetime  
                                      NULL  -- ActionComments - varchar(500)         
									 );  
                    PRINT 'END1'  
     --PRINT('@strMainData - ' + @strMainData)  
     --PRINT('@@WorkGMID - ' + CAST(@WorkGMID AS VARCHAR))  
     --PRINT('@@CampaignID - ' + CAST(@CampaignID AS VARCHAR))  
     --PRINT('@@WorkDMID - ' + CAST(@WorkDMID AS VARCHAR))  
     --PRINT('@@WorkIMID - ' + CAST(@WorkIMID AS VARCHAR))  
     --PRINT('@@ErrorNumber - ' + CAST(@ErrorNumber AS VARCHAR))  
     --PRINT('@@LoginMID - ' + CAST(@LoginMID AS VARCHAR))  
     --PRINT('@@@GlobalUserID - ' + CAST(@GlobalUserID AS VARCHAR))  
     --PRINT('@@@SaveDateTime - ' + CAST(@SaveDateTime AS VARCHAR))  
     --PRINT('@@@HostName - ' + CAST(@HostName AS VARCHAR))  
     --PRINT('@@@StartDateTime - ' + CAST(@StartDateTime AS VARCHAR))  
  
              SET @QueryStr = '  INSERT  INTO dbo.WAT_DataDetail_ALG  
                                    ( WorkGMID ,  
         -- TransactionNO,  
                   CampaignID ,  
                                      WorkDMID ,  
                                      WorkIMID ,  
                  --[Queue] ,--DataValue ,  
                                         StatusDID ,  
                                      LoginMID ,  
                                      GlobalUserID ,  
                                      Status ,  
                                      StartDate ,  
                                      CreatedDateTime ,  
                                      CreatedBy ,  
                                      HostName ,  
                                      ActionStartDateTime ,  
                                      ActionEndDateTime , 
									  ActiveWorkStatusDID ,
									  DataMTID,
          ' + @strMainData + ' )                                    
                                    select ' + CAST(@WorkGMID AS VARCHAR) + '  , -- WorkGMID - int  
               ' + CAST(@CampaignID AS VARCHAR) + ' , -- CampaignID - int  
                                      ' + CAST(@WorkDMID AS VARCHAR) + ' , -- WorkDMID - int  
                                      ' + CAST(@WorkIMID AS VARCHAR) + ' ,  
                                     ' + CAST(@ErrorNumber AS VARCHAR) + ' , -- DataValue - int  
                                      ' + CAST(@LoginMID AS VARCHAR) + ' , -- LoginMID - int  
                                      ' + CAST(@GlobalUserID AS VARCHAR) + ' , -- GlobalUserID - varchar(100)  
                                      0 , -- Status - tinyint  
                                      ''' + @SaveDateTime + ''' , -- StartDate - datetime  
                                      ''' + @SaveDateTime  + ''' , -- CreatedDateTime - datetime  
                                      ' + CAST(@LoginMID AS VARCHAR) + ' , -- CreatedBy - varchar(100)  
                                      ''' + @HostName + ''' , -- HostName - varchar(100),  
                                      ''' + @StartDateTime  + ''' ,  
                                      ''' + @SaveDateTime  + ''' ,  
									  ' + CAST(@ActiveWorkStatusDID AS VARCHAR) + ' ,  
									  ' + CAST(@DataMTID AS VARCHAR) + ' ,  
									
           ' + @strMainData  + '  
  
           FROM  (     
                SELECT  ParaName ,    
                  Value    
                FROM    #tmpScriptData    
               ) AS s PIVOT    
               ( MIN(Value) FOR [ParaName] IN ( ' + @strMainData+ ' ) )AS pvt ';    
        
  
                                 PRINT('aaaaa')  
                            print 'amit kumar 1'
                            PRINT(@QueryStr);  
                            EXEC(@QueryStr);    
                  SET @DataALGID=  @@IDENTITY;  				 
				  IF(@ClientMID=208)
				  BEGIN
					--INSERT INTO WAT_DataDetail_Status_ALG (DataDID,StatusDID,LoginMID,[Status],CreatedBy,CreatedDateTime) 
					--								VALUES (@DataALGID,@ErrorNumber,@LoginMID,0,@LoginMID,GETDATE())
					EXEC proc_WAT_ADDUpdateWorkStatus_ALG @WorkGMID, @DataALGID,@ErrorNumber,@LoginMID,0 
				  END

				 IF(@UMID>0 AND @DataMTID>0)
				 BEGIN
				 EXEC [Proc_WAT_UpdateDataMasterDispositionStatus] @DataMTID
				 END
				   --=====For Send Email for PCC Tracker===========
				  IF(@WorkGMID=45)
				  BEGIN
												
						DECLARE  @WATEmailTmpID INT=0, @Col1 VARCHAR(100),@Col4 VARCHAR(100),@Col5 VARCHAR(100),@Col6 VARCHAR(100),@Col7 VARCHAR(100),@Col8 VARCHAR(100),
						@Col9 VARCHAR(100),@Col10 VARCHAR(100),@Col12 VARCHAR(100),@Col13 VARCHAR(100),@Col14 VARCHAR(100),@AlertType INT=0,
						@Counter INT=0, @SingleMessagesForMultipleError VARCHAR(MAX)=NULL,@MailSubject VARCHAR(1000)=NULL,@MailBody VARCHAR(MAX)=NULL,
						@EmailCC VARCHAR(1000)=NULL,@EmpNameEmail VARCHAR(1000)=NULL,@UserName VARCHAR(1000)=NULL,@MailTo VARCHAR(1000)=NULL, @RegardsName VARCHAR(500)=NULL,
						@CreatedBy VARCHAR(100)=NULL,@WATEmailDetailsID INT=0


						SET @AlertType=26
						--  SET @EmailCC='pramod.negi@teleperformancedibs.com'						 
						 SET @EmailCC='ncleads@intelenetglobal.com;Sachin.chawla@teleperformancedibs.com'	
						--SET @EmailCC='pramod.negi@teleperformancedibs.com;Naveen.Kumar5@intelenetglobal.com'						 
						SELECT @DataDID=IDENT_CURRENT('WAT_DataDetail_ALG')
						SELECT @Col1=Col1,@Col4=Col4,@Col5=Col5, @Col6=Col6 ,@Col7=Col7,@Col8=Col8,@Col9=Col9, @Col10=Col10,@Col12=Col12,@Col13=Col13,@Col14=Col14,@CreatedBy=CreatedBy FROM WAT_DataDetail_ALG where DataDID=IDENT_CURRENT('WAT_DataDetail_ALG')
						 
						DECLARE @FreezeStatus INT=0, @Flagval INT=0
						SET @EmpNameEmail=@Col1  ----FROM Prism_DataMart_MVC.dbo.WAT_DataDetail_ALG WHERE DataDID=@AuditDDID
						SET @UserName= CONCAT(RTRIM(LTRIM(LEFT(@EmpNameEmail, CHARINDEX('(', @EmpNameEmail)-1 ))),',')
						SET @MailTo =SUBSTRING(@EmpNameEmail,CHARINDEX('(',@EmpNameEmail)+1,CHARINDEX(')',@EmpNameEmail) - CHARINDEX('(',@EmpNameEmail)-1) 

						 	
						--IF(@Col4 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =1)) SET @WATEmailTmpID=1  END
						--IF(@Col5 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =2)) SET @WATEmailTmpID=2  END
						--IF(@Col6 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =3)) SET @WATEmailTmpID=3  END
						--IF(@Col7 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =4)) SET @WATEmailTmpID=4  END
						--IF(@Col8 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =5)) SET @WATEmailTmpID=5  END
						--IF(@Col9 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =6)) SET @WATEmailTmpID=6  END
						--IF(@Col10 ='No')BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =7)) SET @WATEmailTmpID=7  END
						----IF(@Col12 ='No')BEGIN SET @FreezeStatus=1 SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =8)) SET @WATEmailTmpID=8  END
						--IF(@Col12 ='No')BEGIN SET @FreezeStatus=1   END
						--IF(@Col13 ='No')BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =9)) SET @WATEmailTmpID=9  END

						 	
					 

						IF(@Col4 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =1)) SET @WATEmailTmpID=1  END
						IF(@Col5 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =2)) SET @WATEmailTmpID=2  END
						IF(@Col6 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =3)) SET @WATEmailTmpID=3  END
						IF(@Col7 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =4)) SET @WATEmailTmpID=4  END
						IF(@Col8 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =5)) SET @WATEmailTmpID=5  END
						IF(@Col9 ='No')	BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =6)) SET @WATEmailTmpID=6  END
						IF(@Col10 ='No')BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =7)) SET @WATEmailTmpID=7  END
						--IF(@Col12 ='No')BEGIN SET @FreezeStatus=1 SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =8)) SET @WATEmailTmpID=8  END
						IF(@Col12 ='No')BEGIN SET @FreezeStatus=1   END
						IF(@Col13 ='No')BEGIN SET @Counter=@Counter+1 SET @SingleMessagesForMultipleError = CONCAT(@SingleMessagesForMultipleError,(SELECT Message FROM WAT_EmailTemplateMaster WHERE WATEmailTmpID =9)) SET @WATEmailTmpID=9  END



						IF(@Counter =1)
						BEGIN										
							IF(@Col4='No') 
							BEGIN 
									SET @WATEmailTmpID=1 
									INSERT INTO [dbo].[WAT_EmailDetails] ([WATEmailTmpID],[DOCDID],[MailFrom],[Mailto],[MailCC],[MailBCC],[Username],[CreatedBy],[CreatedDateTime],[Host],[FreezeStatus])
																	 VALUES(@WATEmailTmpID,@DataDID,'',@MailTo,@EmailCC,'',@LoginMID,@LoginMID,GETDATE(),@HostName,0)
					 				INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
											 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)		
						 
							 END
							IF(@Col5='No') 
							BEGIN 
								SET @WATEmailTmpID=2 
								INSERT INTO [dbo].[WAT_EmailDetails] ([WATEmailTmpID],[DOCDID],[MailFrom],[Mailto],[MailCC],[MailBCC],[Username],[CreatedBy],[CreatedDateTime],[Host],[FreezeStatus])
																 VALUES(@WATEmailTmpID,@DataDID,'',@MailTo,@EmailCC,'',@LoginMID,@LoginMID,GETDATE(),@HostName,0)
					 			INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
									 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)		
							 END
							IF(@Col6='No') 
							BEGIN 
								SET @WATEmailTmpID=3
								INSERT INTO [dbo].[WAT_EmailDetails] ([WATEmailTmpID],[DOCDID],[MailFrom],[Mailto],[MailCC],[MailBCC],[Username],[CreatedBy],[CreatedDateTime],[Host],[FreezeStatus])
																 VALUES(@WATEmailTmpID,@DataDID,'',@MailTo,@EmailCC,'',@LoginMID,@LoginMID,GETDATE(),@HostName,0)
					 			INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
										 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)		
							END
							IF(@Col7='No') 
							BEGIN 
								SET @WATEmailTmpID=4 
									 INSERT INTO [dbo].[WAT_EmailDetails] ([WATEmailTmpID],[DOCDID],[MailFrom],[Mailto],[MailCC],[MailBCC],[Username],[CreatedBy],[CreatedDateTime],[Host],[FreezeStatus])
																	 VALUES(@WATEmailTmpID,@DataDID,'',@MailTo,@EmailCC,'',@LoginMID,@LoginMID,GETDATE(),@HostName,0)
					 				INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
											 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)		
						
							END
							IF(@Col8='No') 
							BEGIN 
								SET @WATEmailTmpID=5 
								 INSERT INTO [dbo].[WAT_EmailDetails] ([WATEmailTmpID],[DOCDID],[MailFrom],[Mailto],[MailCC],[MailBCC],[Username],[CreatedBy],[CreatedDateTime],[Host],[FreezeStatus])
																 VALUES(@WATEmailTmpID,@DataDID,'',@MailTo,@EmailCC,'',@LoginMID,@LoginMID,GETDATE(),@HostName,0)
					 			INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
										 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)		
							END
							IF(@Col9='No') 
							BEGIN
								SET @WATEmailTmpID=6 
								 INSERT INTO [dbo].[WAT_EmailDetails] ([WATEmailTmpID],[DOCDID],[MailFrom],[Mailto],[MailCC],[MailBCC],[Username],[CreatedBy],[CreatedDateTime],[Host],[FreezeStatus])
																 VALUES(@WATEmailTmpID,@DataDID,'',@MailTo,@EmailCC,'',@LoginMID,@LoginMID,GETDATE(),@HostName,0)
					 			INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
										 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)		
							END
							IF(@Col10='No')
							BEGIN 
								SET @WATEmailTmpID=7
								 INSERT INTO [dbo].[WAT_EmailDetails] ([WATEmailTmpID],[DOCDID],[MailFrom],[Mailto],[MailCC],[MailBCC],[Username],[CreatedBy],[CreatedDateTime],[Host],[FreezeStatus])
																 VALUES(@WATEmailTmpID,@DataDID,'',@MailTo,@EmailCC,'',@LoginMID,@LoginMID,GETDATE(),@HostName,0)
					 			INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
										 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)		
						
							END
							IF(@Col12='No')
							BEGIN
								 SET @WATEmailTmpID=8 
								 INSERT INTO [dbo].[WAT_EmailDetails] ([WATEmailTmpID],[DOCDID],[MailFrom],[Mailto],[MailCC],[MailBCC],[Username],[CreatedBy],[CreatedDateTime],[Host],[FreezeStatus])
																 VALUES(@WATEmailTmpID,@DataDID,'',@MailTo,@EmailCC,'',@LoginMID,@LoginMID,GETDATE(),@HostName,1)
					 			--INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
									--	 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)		
							END
							IF(@Col13='No')
							BEGIN 
								SET @WATEmailTmpID=9
								 INSERT INTO [dbo].[WAT_EmailDetails] ([WATEmailTmpID],[DOCDID],[MailFrom],[Mailto],[MailCC],[MailBCC],[Username],[CreatedBy],[CreatedDateTime],[Host],[FreezeStatus])
																 VALUES(@WATEmailTmpID,@DataDID,'',@MailTo,@EmailCC,'',@LoginMID,@LoginMID,GETDATE(),@HostName,0)
					 			INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
										 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)		
						
							END	
							

							
							--INSERT INTO [dbo].[WAT_EmailDetails] ([WATEmailTmpID],[DOCDID],[MailFrom],[Mailto],[MailCC],[MailBCC],[Username],[CreatedBy],[CreatedDateTime],[Host],[FreezeStatus])
							--									 VALUES(@WATEmailTmpID,@DataDID,'',@MailTo,@EmailCC,'',@LoginMID,@LoginMID,GETDATE(),@HostName,@FreezeStatus)
					 		
							--IF((@Col12 != 'No') AND @Col12 != 'Yes')
							--BEGIN
							--    SET @Falg=2
							--END
							--     INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
							--			 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)		
						
							

								
												
							
						SELECT @RegardsName=EmployeeName  FROM dbo.LoginMaster WHERE LoginMID=@CreatedBy
						SET @WATEmailDetailsID=IDENT_CURRENT('[WAT_EmailDetails]')
						SELECT @MailSubject=EmailSubject, @MailBody=EmailBody FROM Prism_DataMart_MVC.dbo.WAT_EmailTemplateMaster WHERE WATEmailTmpID=@WATEmailTmpID												
						SET @MailBody=REPLACE(REPLACE(REPLACE((REPLACE(@MailBody,'<AgentName>',@UserName)),'<ErrorMessage>',@SingleMessagesForMultipleError),'<BindComment>',@Col14),'<AgentRegardsName>',@RegardsName)						 
						UPDATE  WAT_EmailDetails SET MailSubject=@MailSubject,MailBody= @MailBody  WHERE WATEmailDetailsID=@WATEmailDetailsID
	 
					  END --Counter Condition Close here
					  ELSE
						BEGIN
								SET @WATEmailTmpID=10
								 INSERT INTO [dbo].[WAT_EmailDetails] ([WATEmailTmpID],[DOCDID],[MailFrom],[Mailto],[MailCC],[MailBCC],[Username],[CreatedBy],[CreatedDateTime],[Host],[FreezeStatus])
																 VALUES(@WATEmailTmpID,@DataDID,'',@MailTo,@EmailCC,'',@LoginMID,@LoginMID,GETDATE(),@HostName,0)
					 		

							--IF((@Col12 = 'No') OR @Col12 = 'Yes')
							--BEGIN
							--    SET @Flagval=2
							--END
							SET @SingleMessagesForMultipleError='<ol>'+@SingleMessagesForMultipleError+'</ol>'
							
							     INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
										 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)		
						

								--INSERT INTO QMS_MVC.[dbo].[AuditAlertDetail] ([AuditDDID],[AlertATID],[Flag],[CreatedBy],[CreatedDateTime],[Host])
								--		 VALUES (@DataDID,@AlertType,0,@LoginMID,GETDATE(),@HostName)
						
						SELECT @RegardsName=EmployeeName  FROM dbo.LoginMaster WHERE LoginMID=@CreatedBy
						SET @WATEmailDetailsID=IDENT_CURRENT('[WAT_EmailDetails]')
						SELECT @MailSubject=EmailSubject, @MailBody=EmailBody FROM Prism_DataMart_MVC.dbo.WAT_EmailTemplateMaster WHERE WATEmailTmpID=@WATEmailTmpID												
						SET @MailBody=REPLACE(REPLACE(REPLACE((REPLACE(@MailBody,'<AgentName>',@UserName)),'<ErrorMessage>',@SingleMessagesForMultipleError),'<BindComment>',@Col14),'<AgentRegardsName>',@RegardsName)						 
						UPDATE  WAT_EmailDetails SET MailSubject=@MailSubject,MailBody= @MailBody  WHERE WATEmailDetailsID=@WATEmailDetailsID
	 
						END	
				  END
				  --=================Close========================
                        END;                     
                    SET @ErrorNumber = 1;        
                END;  
            ELSE  
			
                BEGIN  
                    INSERT  INTO dbo.WAT_DataDetail_ALGLog  
                            SELECT  @SaveDateTime ,  
                                    @LoginMID ,  
                                    @HostName ,  
                                    *  
                            FROM    dbo.WAT_DataDetail_ALG  
                            WHERE   DataDID = @DataDID;                                
   
      
       SET @ErrorNumber = 2;  
 END;        
  
     -- Comment Date 27 August 2020

     --       IF ( SELECT COUNT(*)  
     --            FROM   dbo.WAT_AgentWorkDetails  
     --            WHERE  LoginMID = @LoginMID  
     --                   AND WorkDMID = @WorkDMID  
     --                   AND WorkIMID = @WorkIMID  
     --                   AND CampaignID = @CampaignID  
     --                   AND CAST(ActivityDate AS DATE) = CAST(GETDATE() AS DATE)  
     --          ) < 1  
     --           BEGIN  
		 
     --               INSERT  INTO dbo.WAT_AgentWorkDetails  
     --                       ( ActivityDate ,  
     --                         LoginMID ,  
     --                         GlobalUserID ,  
     --                         WorkIMID ,  
     --                         TodayWork ,  
     --                         FreezeStatus ,  
     --                         CreatedDateTime ,  
     --                         CreatedBy ,  
     --                         HostName ,  
     --                         CompletedWork ,  
     --                         WorkDMID ,  
     --                         WorkGMID ,  
     --                         CampaignID ,  
     --                         UpdatedDateTime ,  
     --                         UpdatedBy  
     --                       )  
     --               VALUES  ( GETDATE() ,  
     --                         @LoginMID ,  
     --                         @GlobalUserID ,  
     --                         @WorkIMID ,  
     --                         1 ,  
     --                         0 ,  
     --                         GETDATE() ,  
     --                         @LoginMID ,  
     --                         '' ,  
     --                         1 ,  
     --                         @WorkDMID ,  
     --                         @WorkGMID ,  
     --                         @CampaignID ,  
     --                       GETDATE() ,  
     --        @LoginMID  
     --                       );  
     --           END;  
     --       ELSE  
     --           BEGIN  				 
					--IF (@CatID=1 AND @Assignment=1)
					--BEGIN
					--	 UPDATE  WAT_AgentWorkDetails  
					--	SET    
					--			CompletedWork = ISNULL(CompletedWork,0) + 1  
					--	WHERE   LoginMID = @LoginMID  
					--			AND WorkDMID = @WorkDMID  
					--			AND WorkIMID = @WorkIMID  
					--		AND CampaignID = @CampaignID  
					--			AND CAST(ActivityDate AS DATE) = CAST(GETDATE() AS DATE);  
					--END					 
     --           END;  
           
        END TRY  
        BEGIN CATCH  
            SET @ErrorNumber = 0;  
        END CATCH;                      
    END;  
  
 

GO

/****** Object:  StoredProcedure [dbo].[Proc_WAT_LiveTMEasyjetDashbaord]    Script Date: 9/27/2021 4:02:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Mahesh Mamgain>
-- Create date: <2021-09-08>
-- Description:	<Create a dasboard for WAT TM Dashboard for specific Process>
-- =============================================
--exec Proc_WAT_LiveTMEasyjetDashbaord @DateFrom=N'2021-09-24',@DateTo=N'2021-09-24',@ClientIDs=N'YF@~@DRl0Rw0U=',@TMIDs=N'',@AgentIDs=N'',@WorkItemIDs=N'',@LoginMID=N'22',@AccessType=N'5'
--exec Proc_WAT_LiveTMEasyjetDashbaord @DateFrom=N'2021-09-24',@DateTo=N'2021-09-24',@ClientIDs=N'm96S27X6NVQ=',@TMIDs=N'',@AgentIDs=N'',@WorkItemIDs=N'',@LoginMID=N'22',@AccessType=N'5'
--exec Proc_WAT_LiveTMEasyjetDashbaord @DateFrom=N'2021-08-01',@DateTo=N'2021-09-24',@ClientIDs=N'YF@~@DRl0Rw0U=',@TMIDs=N'fGmIEm4wxZw=',@AgentIDs=N'Orcp0OzH4rk=',@WorkItemIDs=N'tp3@~@cCcHzyo=',@LoginMID=N'22',@AccessType=N'5'
--exec Proc_WAT_LiveTMEasyjetDashbaord @DateFrom=N'2021-08-01',@DateTo=N'2021-09-23',@ClientIDs=N'YF@~@DRl0Rw0U=',@TMIDs=N'',@AgentIDs='',@WorkItemIDs='', @LoginMID=N'22100',@AccessType=N'9'
--exec Proc_WAT_LiveTMEasyjetDashbaord @DateFrom=N'',@DateTo=N'',@ClientIDs=N'YF@~@DRl0Rw0U=',@TMIDs=N'5186',@AgentIDs=N'5049',@WorkItemIDs=N'1519',@LoginMID=N'22',@AccessType=N'5'
--exec Proc_WAT_LiveTMEasyjetDashbaord @DateFrom=default,@DateTo=default,@ClientIDs=N'',@TMIDs=N'',@AgentIDs=N'',@WorkItemIDs=N'',@LoginMID=N'22',@AccessType=N'5'


CREATE PROCEDURE [dbo].[Proc_WAT_LiveTMEasyjetDashbaord]
	-- Add the parameters for the stored procedure here
	  @DateFrom DATETIME = '1900-01-01 00:00:00.000' ,
      @DateTo DATETIME = '1900-01-01 00:00:00.000' ,
	  @ClientIDs VARCHAR(100) = '' ,
	  @TMIDs Varchar(2000)='',
	  @AgentIDs Varchar(2000)='',
	  @WorkItemIDs VARCHAR(100)='',
	  @LoginMID VARCHAR(100) = '' ,
      @AccessType INT = 0
   
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;
		DECLARE @trans varchar(MAX)
		set @trans ='SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
		'  
	    DECLARE @ErrorNumber INT
        DECLARE @ErrorMessage VARCHAR(MAX)
        SET @ErrorNumber = 0 
        SET @ErrorMessage = 'Failed to extract WAT TM Dashbaord dashbaord data following error.. ;'
		
		 -- SET @DateFrom  = '2021-08-20 00:00:00.000' 
		 -- SET @DateTo  = '2021-09-20 00:00:00.000' 
		

		--SET  @ClientIDs  = 'YF@~@DRl0Rw0U=' 		
		SET @ClientIDs = ISNULL(dbo.fn_str_FROM_BASE64(@ClientIDs), '')
		SET @TMIDs=ISNULL(dbo.fn_str_FROM_BASE64(@TMIDs), '')
		SET @AgentIDs = ISNULL(dbo.fn_str_FROM_BASE64(@AgentIDs), '')
		SET @WorkItemIDs = ISNULL(dbo.fn_str_FROM_BASE64(@WorkItemIDs), '')
		
		DECLARE @MainStr VARCHAR(MAX) = ''
		DECLARE @ClientVC VARCHAR(Max) = ''
		DECLARE @CampaignVC VARCHAR(Max) = ''
        DECLARE @TMVC VARCHAR(500) = ''
        DECLARE @AgentVC VARCHAR(500) = ''
        DECLARE @WorkItemVC VARCHAR(500) = ''
	    DECLARE @AccessVC VARCHAR(200) = ''
        DECLARE @DateVC VARCHAR(200) = ''
		Declare @StartD Datetime
		Declare @EndD Datetime

		;WITH ListDates(AllDates) AS
		(    SELECT DATEADD(DAY,0,@DateFrom) AS DATE
			UNION ALL
			SELECT DATEADD(DAY,1,AllDates)
			FROM ListDates 
			WHERE AllDates <= DATEADD(DAY,-1,@DateTo))
		SELECT AllDates
		INTO #AllDates FROM ListDates
		
		--;with hrcte as
		--(
		--	SELECT 0 AS AllHour
		--	UNION ALL
		--	SELECT AllHour + 1
		--	FROM hrcte 
		--	WHERE AllHour + 1 < 24
		--)
		--SELECT hrcte.AllHour INTO #AllTime FROM hrcte		

		--SELECT AllDates,AllHour INTO #AllDateHours FROM #AllDates CROSS JOIN #AllTime
		
		--IF OBJECT_ID(N'dbo.#AllDates', N'U') IS NOT NULL  
		--   DROP TABLE #AllDates;  
		
		--IF OBJECT_ID(N'dbo.#AllTime', N'U') IS NOT NULL  
		--   DROP TABLE #AllTime;  
		IF(@DateFrom = '1900-01-01 00:00:00.000')
		BEGIN
		SET @DateFrom=CONVERT(CHAR(10), Getdate() ,126) + ' 00:00:00.000'
		SET @DateTo=CONVERT(CHAR(10), Getdate() ,126) + '  00:00:00.000'
		END

		set @StartD=cast(@DateFrom as datetime)
		set @EndD=cast(DATEADD(DD, 1,@DateTo) as datetime)
           
		 IF ( @StartD <> '1900-01-01 00:00:00.000' )
                BEGIN 
                  
				  
				  SET @DateVC = ' and  WM.CreatedDateTime BETWEEN '''
                                + CAST(@StartD AS VARCHAR) + ''' AND '''
                                + CAST(@EndD AS VARCHAR) + ''''
                     
                END
            ELSE
                SET @DateVC = ' '

			IF (@ClientIDs <> '')
            BEGIN      
                SET @ClientVC = ' AND  UM.ClientID  IN ('+@ClientIDs+') '        
            END
			ELSE
			BEGIN
			   SET @ClientIDs=(Select DefaultClientID FROM Prism_DataMart_MVC.dbo.LoginMaster WHERE LoginMID = @LoginMID AND FreezeStatus=0)		
			   SET @ClientVC = ' AND  UM.ClientID  IN ('+@ClientIDs+') '		
			END

			--for data detail table
			IF (@ClientIDs <> '')
            BEGIN      
                SET @CampaignVC = ' AND  WD.CampaignID  IN (SELECT DISTINCT CampaignID FROM EmeraldHierarchies_MVC.dbo.Campaigns WHERE ProjectID IN (
										SELECT ProjectID from EmeraldHierarchies_MVC.dbo.Projects WHERE ClientID IN ('+@ClientIDs+') AND IsActive=1) AND IsActive=1) '        
            END
			ELSE
			BEGIN
			   SET @ClientIDs=(Select DefaultClientID FROM Prism_DataMart_MVC.dbo.LoginMaster WHERE LoginMID = @LoginMID AND FreezeStatus=0)		
			    SET @CampaignVC = ' AND  WD.CampaignID  IN (SELECT DISTINCT CampaignID FROM EmeraldHierarchies_MVC.dbo.Campaigns WHERE ProjectID IN (
										SELECT ProjectID from EmeraldHierarchies_MVC.dbo.Projects WHERE ClientID IN ('+@ClientIDs+') AND IsActive=1) AND IsActive=1) '        	
			END

			IF (@TMIDs <> '')
            BEGIN      
                SET @TMVC = ' AND  GU.GlobalUserID  IN ('+@TMIDs+') '        
            END
			ELSE
			BEGIN      
                SET @TMVC = ''        
            END

			IF (@AgentIDs <> '')
            BEGIN      
                SET @AgentVC = ' AND  LM.GlobalUserID  IN ('+@AgentIDs+') '        
            END
			ELSE
			BEGIN      
                SET @AgentVC = ''        
            END

			IF (@WorkItemIDs <> '')
            BEGIN      
                SET @WorkItemVC = ' AND  WD.WorkIMID  IN ('+@WorkItemIDs+') '        
            END
			ELSE
			BEGIN      
                SET @WorkItemVC = ''        
            END

			 

			--- Total Unique Case Uploaded, Case Processed, Case Pending, 
			
		SET @MainStr='
						SELECT WM.DataMTID,WM.UMID,WM.[Date],WM.PNR,WM.DispositionStatus,WM.DispositionDate,
						WM.DispositionBy AS AgentGlobalUserID,
						WM.CreatedDateTime 
						,UM.ClientID, UM.[Name] AS WorkItemQueue				
						INTO #RAWDATA  FROM dbo.WAT_DataMaster WM 
						INNER JOIN uploadMaster UM ON WM.UMID=UM.UMID AND UM.FreezeStatus=0
						JOIN WAT_WorkDivisionWorkItemDetail WD ON WM.UMID=WD.UMID
						LEFT JOIN LoginMaster LM ON WM.DispositionBy=LM.LoginMID and LM.FreezeStatus=0
						LEFT JOIN [EmeraldHierarchies_MVC].dbo.GlobalUserRoles GUR ON GUR.GlobalUserID = LM.GlobalUserID AND GUR.FreezeStatus=0 
						LEFT JOIN [EmeraldHierarchies_MVC].dbo.GlobalUsers GU ON GU.GlobalUserID = GUR.ParentGlobalUserID
						WHERE WM.FreezeStatus=0 '+ @DateVC + @ClientVC + @WorkItemVC +'
						
						SELECT WD.DataDID,WD.WorkGMID,WD.WorkIMID,WD.OutcomeMID,WD.StatusDID,WD.LoginMID,WD.GlobalUserID,WD.CreatedDateTime,
						WD.ActiveWorkStatusDID,WD.DataMTID,LM.EmployeeName AS AgentName,  GU.FirstName + '' '' + GU.LastName AS [TM] , GU.GlobalUserID AS ParentGlobalUserID	
						INTO  #WatDataDetail 
						FROM WAT_DataDetail_ALG WD 
						LEFT JOIN LoginMaster LM ON WD.CreatedBy=LM.LoginMID and LM.FreezeStatus=0
						LEFT JOIN [EmeraldHierarchies_MVC].dbo.GlobalUserRoles GUR ON GUR.GlobalUserID = LM.GlobalUserID AND GUR.FreezeStatus=0 
						LEFT JOIN [EmeraldHierarchies_MVC].dbo.GlobalUsers GU ON GU.GlobalUserID = GUR.ParentGlobalUserID
						WHERE WD.FreezeStatus=0
						' + REPLACE(@DateVC,'WM.','WD.') + @CampaignVC + @TMVC + @AgentVC + @WorkItemVC +'
						

						SELECT WD.StatusDID,WD.LoginMID,WD.ActionSMID,WD.WorkIMID,WD.ActiveWorkStatusDID,WD.ActionStartDateTime,WD.ActionEndDateTime,WD.ElapsedTime,
						WD.DataMTID,DD.AgentName,DD.[TM],DD.ParentGlobalUserID
						INTO #WatStatusDetail  FROM WAT_ActionStatusDetail_ALG WD 
						JOIN #WatDataDetail DD ON WD.DataMTID=DD.DataMTID
						WHERE WD.ActiveWorkStatusDID IS NOT NULL			
						' + REPLACE(@DateVC,'WM.CreatedDateTime','WD.ActionEndDateTime') + @CampaignVC + REPLACE(@TMVC,'GU.GlobalUserID','DD.ParentGlobalUserID') + REPLACE(@AgentVC,'LM.','DD.')
						 + @WorkItemVC +'

						-----	Table 0	   -------
					     SELECT TotalUniqueCaseUploaded=(SELECT Count(*) FROM #RAWDATA)
						,CaseProcessed=(SELECT Count(*) FROM #WatDataDetail )
						,CasePending=(SELECT Count(*) FROM WAT_DataMaster WHERE DispositionStatus=0 )
						,CaseInProcess=(SELECT Count(*) FROM WAT_DataMaster WHERE DispositionStatus=1 )	
						,ProductiveAux=(SELECT CONVERT(varchar(8), DATEADD(ms, Sum(ElapsedTime)* 1000, 0), 114) AS ProductiveAux FROM #WatStatusDetail WHERE ActionSMID IN (2,7))
														
						------	Table 1- Agent Wise Case Count	-------	------	Table 2- TM Wise Case Count	  --------
						IF EXISTS ( SELECT * FROM #WatDataDetail)
							BEGIN
								SELECT DISTINCT AgentName, Count(*) AS CaseCount
										FROM #WatDataDetail
										GROUP BY AgentName
										ORDER BY Count(*) DESC
								SELECT DISTINCT [TM],Count(*)  AS CaseCount
								FROM #WatDataDetail
								GROUP BY [TM]
								ORDER BY Count(*) DESC
							END
							ELSE
							BEGIN
							  SELECT '''' as AgentName,  0 as CaseCount
							  SELECT '''' as [TM],  0 as CaseCount
							END
					

						
						'
	SET @MainStr = @MainStr + 
					'	------	Table 3- Queue Wise Unique Case Uploaded --------
						IF EXISTS ( SELECT * FROM #RAWDATA)
							BEGIN
							SELECT WorkItemQueue,Count(*) AS CaseCount FROM #RAWDATA
							GROUP BY WorkItemQueue ORDER BY WorkItemQueue
							END
						ELSE
							SELECT '''' AS WorkItemQueue, 0 AS CaseCount
						------	Table 4- Queue Wise Unique Case Processed -------- Not In Used
						SELECT WorkItemQueue,Count(*) AS CaseCount FROM #RAWDATA WHERE DispositionStatus=2
						GROUP BY WorkItemQueue ORDER BY WorkItemQueue
						
						------	Table 5- Queue Wise Case Pending --------
						
						SELECT UM.[Name] AS WorkItemQueue, Count(*) AS CaseCount FROM WAT_DataMaster WD INNER JOIN
						uploadMaster UM ON WD.UMID=UM.UMID
						WHERE WD.DispositionStatus=0
						GROUP BY UM.[Name] ORDER BY UM.[Name]

				    	------	Table 6- Agent Wise EPH --------
						SELECT DISTINCT WSD.AgentName, WSD.LoginMID,WSD.WorkIMID,WSD.ActiveWorkStatusDID,WSD.DataMTID,
						Min(WSD.ActionStartDateTime) AS ActionStartDateTime,
						Max(WSD.ActionEndDateTime) AS ActionEndDateTime,
						Sum(WSD.ElapsedTime) AS TotalTime,
						WM.WorkItemName AS QueueName
						INTO #ProductiveTemp  from #WatStatusDetail   WSD
						JOIN WAT_WorkItemMaster WM ON WSD.WorkIMID=WM.WorkIMID
						WHERE ActionSMID IN (2,7)
						GROUP BY  WSD.AgentName,  WSD.LoginMID, WSD.WorkIMID, WSD.ActiveWorkStatusDID, WSD.DataMTID,WM.WorkItemName
					   	
						IF EXISTS ( SELECT * FROM #ProductiveTemp)
							BEGIN
					    select AgentName, LoginMID,QueueName, CONVERT(NVARCHAR, ActionEndDateTime, 106) AS DataDate, 
						Sum(TotalTime) AS TotalTimeInSeconds,
						cast(Sum(TotalTime) / (60.00*60) as decimal(6, 0)) AS TotalHrs,
						COUNT(Distinct DataMTID) AS TotalCaseCount
						from #ProductiveTemp  
						GROUP BY AgentName, LoginMID,  CONVERT(NVARCHAR, ActionEndDateTime, 106),QueueName
						Order by AgentName, CONVERT(NVARCHAR, ActionEndDateTime, 106), QueueName
							END
						ELSE
						select '''' AgentName, '''' LoginMID,'''' QueueName, '''' DataDate, 
						0 AS TotalTimeInSeconds,
						0 AS TotalHrs,
						0 AS TotalCaseCount


						SELECT DISTINCT Convert(VARCHAR(10), ActionEndDateTime,105) AS DataDate, 
						DATEPART(HOUR, ActionEndDateTime)  AS AHour ,
						Sum(TotalTime) AS TotalTime,
						COUNT(Distinct DataMTID) AS CaseCount
						INTO #DateWiseProductive from #ProductiveTemp
						GROUP BY Convert(VARCHAR(10), ActionEndDateTime,105),	DATEPART(HOUR, ActionEndDateTime)
		
						 SELECT CONVERT(NVARCHAR, ADH.AllDates, 106) AS AllDates, PD.AHour,PD.CaseCount  --PD.TotalTime 
						 INTO #AllDateProductive FROM #AllDates ADH 
						 LEFT JOIN #DateWiseProductive PD ON  Convert(VARCHAR(10), ADH.AllDates,105)=PD.DataDate
						--Queue Wise Unique Case Processed Tabular-----
					select *
						from 
						(
						  select AllDates, AHour, CaseCount
						  from #AllDateProductive  	
						) src
						pivot
						(
						 Sum(CaseCount) 
						  for AHour in ([0],[1], [2], [3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23])
						) piv;
					'

 PRINT ( @MainStr )
 EXEC ( @MainStr )

END

GO



/****** Object:  StoredProcedure [dbo].[proc_WAT_FetchDynamicControlValue]    Script Date: 9/27/2021 4:02:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

      
  -- =============================================    
-- Author:  <Mahesh Mamgain>    
-- Create date: <23-August-21>    
-- =============================================    
    
--EXEC [proc_WAT_FetchDynamicControlValue] @DataMTID='806', @UMID=90  
--EXEC [proc_WAT_FetchDynamicControlValue] @DataMTID='807', @UMID=90  
--EXEC [proc_WAT_FetchDynamicControlValue] @DataMTID='808', @UMID=90  
--exec proc_WAT_FetchDynamicControlValue @DataMTID=N'',@UMID=N'91',@LoginMID=N'24930'
CREATE PROCEDURE [dbo].[proc_WAT_FetchDynamicControlValue]      
   @DataMTID BIGINT,      
   @UMID INT,
   @LoginMID VARCHAR(100)
    
     
AS      
    BEGIN      
      
    DECLARE @ColumnName VARCHAR(MAX)=''    
    DECLARE @DateColumnName VARCHAR(200)=''    
    DECLARE @TotalRecord INT   
    DECLARE @Counter INT  
    DECLARE @DataColumnvalue varchar(100)=''  
    DECLARE @textstr varchar(max)=''  
    DECLARE @textupdate varchar(max)=''  
    DECLARE @ExistDataMTID BIGINT=0
    DECLARE @ExistUMID BIGINT=0
  IF OBJECT_ID('tempdb..#Temp') IS NOT NULL      
                DROP TABLE #Temp;        
    
       BEGIN TRY    
      SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
 CREATE Table #Temp  
(   ID INT IDENTITY(1, 1),   
    UplaodDataCMID int,   
 DBColumnName Varchar(100),  
    DataMDBColumnName Varchar(100),  
 DispositionColumnName Varchar(100),  
 UMID INT,  
 ColumnValue varchar(100)  
      
)  

IF((SELECT Count(*) FROM WAT_ActionStatusDetail_ALG WHERE LoginMID=@LoginMID)>0)
	BEGIN
		SELECT @ExistDataMTID=DataMTID FROM WAT_ActionStatusDetail_ALG WHERE StatusDID IN (SELECT MAX(StatusDID) FROM  WAT_ActionStatusDetail_ALG WHERE LoginMID=@LoginMID)
		IF((SELECT Count(*) from WAT_DataDetail_ALG where DataMTID = @ExistDataMTID)>0)
			BEGIN
			SET @DataMTID=@DataMTID
			END
		ELSE
			BEGIN
			SET @DataMTID=@ExistDataMTID
			SELECT @UMID=UMID FROM WAT_DataMaster WHERE DataMTID=@DataMTID
			END
END
  
insert into #Temp   
select UploadDataCMID,DBColumnName,DataMDBColumnName,DispositionColumnName,UMID,'' from UploadDataColumnMapping where UMID=@UMID and IsVisibleDataOnDisposition=1 and ControlType=0  
  
Select  @TotalRecord=count(*)  from #Temp  
  
SET @Counter=1  
WHILE ( @Counter <= @TotalRecord)  
BEGIN  
  
select  @ColumnName= DataMDBColumnName from #Temp where ID =@Counter  
select @DataColumnvalue = @ColumnName from WAT_DataMaster where DATAMTID=@DataMTID and UMID=@UMID  
set @textstr='select '+@DataColumnvalue+' from WAT_DataMaster where DATAMTID='+Cast(@DataMTID as varchar(10))+' and UMID='+Cast(@UMID as varchar(10))+''  
set @DataColumnvalue=@textstr  
set @textupdate= 'update #Temp set ColumnValue= ('+ (@textstr) + ') where ID ='+Cast(@Counter as varchar(10))+''  
 print (@textupdate)  
 EXEC(@textupdate)   
 SET @Counter  = @Counter + 1  
END  
  
SELECT * from #Temp  
SELECT @DataMTID   AS DataMTID, @UMID AS UMID
  END TRY      
        BEGIN CATCH      
  ROLLBACK TRAN    
         SELECT   ERROR_MESSAGE()    
        END CATCH;                          
    END;      
    
GO


  


/****** Object:  StoredProcedure [dbo].[proc_WAT_Fetch_UploadDataColumnMapping]    Script Date: 9/27/2021 4:02:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

          
  -- =============================================        
-- Author:  <Mahesh Mamgain>        
-- Create date: <27-August-21>        
-- =============================================        
--EXEC [proc_WAT_Fetch_UploadDataColumnMapping] @UMID=18        
--EXEC [proc_WAT_Fetch_UploadDataColumnMapping] @UMID=''        
CREATE PROCEDURE [dbo].[proc_WAT_Fetch_UploadDataColumnMapping]   (   @UMID INT,   @LoginMID VARCHAR(10) =''        
  )        
AS          
    BEGIN          
       DECLARE @SaveDateTime DATETIME = GETDATE();          
     BEGIN TRY 
	 
	 if(@UMID='0' or @UMID='')

	 begin

          
  SELECT CMD.ClientName,        
     cmd.ClientID,        
     UM.Name,          
     UCM.UploadDataCMID,        
     UCM.UMID,        
     UCM.DBColumnName,        
     UCM.DataMDBColumnName,        
     UCM.IsVisibleLabelOnATScreen,        
     CASE WHEN UCM.IsVisibleLabelOnATScreen=1 THEN 'Yes' ELSE 'No' END AS IsVisibleLabelOnATScreenText,        
     UCM.LabelNameOnATScreen,  
  
  UCM.IsVisibleDataOnDisposition,        
     CASE WHEN UCM.IsVisibleDataOnDisposition=1 THEN 'Yes' ELSE 'No' END AS IsVisibleDataOnDispositionText,   
    
    
  UCM.DispositionColumnName,        
  
  ISNULL(UCM.ControlType,0) AS ControlType,  
     CASE WHEN UCM.ControlType=0 THEN 'TextBox' WHEN UCM.ControlType=1 THEN 'DropdownList'  
  WHEN UCM.ControlType=2 THEN 'TextArea'  
  WHEN UCM.ControlType=7 THEN 'RadioButton'  
  WHEN UCM.ControlType=4 THEN 'DateTime'  
  WHEN UCM.ControlType=5 THEN 'DateNTime'  
  ELSE '' END AS DispositionControlTypeText,   
  
    
     UCM.FreezeStatus,         
     CASE WHEN UCM.FreezeStatus=0 THEN 'Active' ELSE 'InActive' END AS FreezeStatusText        
     FROM  UploadDataColumnMapping UCM         
  JOIN uploadMaster UM ON UCM.UMID=UM.UMID AND UM.FreezeStatus=0        
  JOIN ClientMasterDetail CMD ON UM.ClientID=CMD.ClientID         
  WHERE  UCM.FreezeStatus=0  
  
  end

  else


  begin
  SELECT CMD.ClientName,        
     cmd.ClientID,        
     UM.Name,          
     UCM.UploadDataCMID,        
     UCM.UMID,        
     UCM.DBColumnName,        
     UCM.DataMDBColumnName,        
     UCM.IsVisibleLabelOnATScreen,        
     CASE WHEN UCM.IsVisibleLabelOnATScreen=1 THEN 'Yes' ELSE 'No' END AS IsVisibleLabelOnATScreenText,        
     UCM.LabelNameOnATScreen,  
  
  UCM.IsVisibleDataOnDisposition,        
     CASE WHEN UCM.IsVisibleDataOnDisposition=1 THEN 'Yes' ELSE 'No' END AS IsVisibleDataOnDispositionText,   
    
    
  UCM.DispositionColumnName,        
  
  ISNULL(UCM.ControlType,0) AS ControlType,  
     CASE WHEN UCM.ControlType=0 THEN 'TextBox' WHEN UCM.ControlType=1 THEN 'DropdownList'  
  WHEN UCM.ControlType=2 THEN 'TextArea'  
  WHEN UCM.ControlType=7 THEN 'RadioButton'  
  WHEN UCM.ControlType=4 THEN 'DateTime'  
  WHEN UCM.ControlType=5 THEN 'DateNTime'  
  ELSE '' END AS DispositionControlTypeText,   
  
    
     UCM.FreezeStatus,         
     CASE WHEN UCM.FreezeStatus=0 THEN 'Active' ELSE 'InActive' END AS FreezeStatusText        
     FROM  UploadDataColumnMapping UCM         
  JOIN uploadMaster UM ON UCM.UMID=UM.UMID AND UM.FreezeStatus=0        
  JOIN ClientMasterDetail CMD ON UM.ClientID=CMD.ClientID         
  WHERE UCM.UMID=@UMID AND UCM.FreezeStatus=0  


  end
         
        
  END TRY          
        BEGIN CATCH          
  ROLLBACK TRAN        
         SELECT   ERROR_MESSAGE()        
        END CATCH;                              
    END; 
GO

/****** Object:  StoredProcedure [dbo].[proc_WAT_AddUpdate_UploadDataColumnMapping]    Script Date: 9/27/2021 4:02:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

                
  -- =============================================              
-- Author:  <Mahesh Mamgain>              
-- Create date: <27-August-21>              
-- =============================================              
              
CREATE PROCEDURE [dbo].[proc_WAT_AddUpdate_UploadDataColumnMapping]   (            
@UploadDataCMID INT=0,               
@UMID INT,               
@FormatedTableColumnName VARCHAR(200),               
@DataDBColumnName VARCHAR(200),               
@IsvisibleOnActivityTracker TINYINT,               
@LableName VARCHAR(200),               
@IsVisibleDataOnDisposition TINYINT,               
@DispositionColumnName VARCHAR(200),      
@ControlType TINYINT,      
@CreatedBy VARCHAR(50),               
@Host VARCHAR(200),               
@FreezeStatus BIT,              
@AddEdit VARCHAR(10) --//A FOR Add, E for Edit              
                  
  )              
AS                
    BEGIN                
       DECLARE @SaveDateTime DATETIME = GETDATE();               
   DECLARE @ErrorNumber BIGINT=0;  
   DECLARE @UploadDataCMIDIdentity BIGINT=0   
    DECLARE @ErrorMsg VARCHAR(MAX)=''          
     BEGIN TRY                   
     IF(@AddEdit='A' AND @UploadDataCMID=0)              
  BEGIN              
 IF((Select Count(*) FROM UploadDataColumnMapping WHERE UMID=@UMID AND DataMDBColumnName=@DataDBColumnName AND FreezeStatus=0 )>0)          
  BEGIN          
   SET @ErrorNumber = 3                
   SET @ErrorMsg = 'Column Name already Exist'               
          
   SELECT @ErrorNumber AS ErrorNumber, @ErrorMsg AS ErrorMessage          
  END          
  ELSE          
 BEGIN          
   INSERT INTO UploadDataColumnMapping (UMID,              
           DBColumnName,              
           DataMDBColumnName,              
           IsVisibleLabelOnATScreen,              
           LabelNameOnATScreen,         
           IsVisibleDataOnDisposition,        
           DispositionColumnName,      
           ControlType,      
           FreezeStatus,              
           CreatedDateTime,              
           CreatedBy,              
           Host)              
           VALUES(              
           @UMID,              
           @FormatedTableColumnName,              
           @DataDBColumnName,              
           @IsvisibleOnActivityTracker,              
           @LableName,        
           @IsVisibleDataOnDisposition,        
           '',      
           @ControlType,      
           @FreezeStatus,              
           @SaveDateTime,              
           @CreatedBy,              
           @Host              
           )      
       
      SET @UploadDataCMIDIdentity = SCOPE_IDENTITY();  
   Declare @str varchar(max)=''  
            Set @str='Update UploadDataColumnMapping set DispositionColumnName='''+@DispositionColumnName+''' where UploadDataCMID='+CAST(@UploadDataCMIDIdentity AS VARCHAR(10))+''  
           --print(@str)  
            EXEC (@str)  
  
     
      SET @ErrorNumber = 1                
      SET @ErrorMsg = 'Data has been saved successfully'               
          
   SELECT @ErrorNumber AS ErrorNumber, @ErrorMsg AS ErrorMessage          
  END          
  END              
  ELSE IF (@AddEdit='E' AND @UploadDataCMID<>0)              
  BEGIN              
  UPDATE UploadDataColumnMapping SET DBColumnName=@FormatedTableColumnName,              
           DataMDBColumnName=@DataDBColumnName,              
           IsVisibleLabelOnATScreen=@IsvisibleOnActivityTracker,              
           LabelNameOnATScreen=@LableName,        
           IsVisibleDataOnDisposition=@IsVisibleDataOnDisposition,        
           DispositionColumnName=@DispositionColumnName,       
           ControlType=@ControlType,      
           FreezeStatus=@FreezeStatus,              
           UpdatedDateTime=@SaveDateTime,              
           UpdatedBy=@CreatedBy               
  WHERE UploadDataCMID=@UploadDataCMID AND UMID=@UMID    
    
            Declare @strUpdate varchar(max)=''  
            Set @strUpdate='Update UploadDataColumnMapping set DispositionColumnName='''+@DispositionColumnName+''' where UploadDataCMID='+CAST(@UploadDataCMID AS VARCHAR(10))+''  
           --print(@str)  
            EXEC (@strUpdate)  
  
            
        SET @ErrorNumber = 2                
     SET @ErrorMsg = 'Data has been updated successfully'            
     SELECT @ErrorNumber AS ErrorNumber, @ErrorMsg AS ErrorMessage          
  END              
  END TRY                
        BEGIN CATCH                
  ROLLBACK TRAN              
         SELECT  ERROR_NUMBER() as ErrorNumber, ERROR_MESSAGE() AS ErrorMessage            
          
        END CATCH;                                    
    END; 
GO

/****** Object:  StoredProcedure [dbo].[proc_WAT_FetchUMIDAvailableStatus]    Script Date: 9/27/2021 4:02:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

        
  -- =============================================      
-- Author:  <Mahesh Mamgain>      
-- Create date: <23-August-21>      
-- =============================================      
      
--EXEC [proc_WAT_FetchCaseFromDataMasterFIFO] @WorkIMID='1519',@LoginMID='22'      
CREATE PROCEDURE [dbo].[proc_WAT_FetchUMIDAvailableStatus]        
   @WorkIMID INT=0        
    
AS        
    BEGIN        
               
			 select ISNULL(UMID,0) as UMID from   WAT_WorkDivisionWorkItemDetail WHERE WorkIMID=@WorkIMID
    END;        
GO

/****** Object:  StoredProcedure [dbo].[proc_WAT_FetchCaseSectorFromFormatedData]    Script Date: 9/27/2021 4:02:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
  -- =============================================
-- Author:		<Mahesh Mamgain>
-- Create date: <23-August-21>
-- =============================================

--EXEC [proc_WAT_FetchCaseSectorFromFormatedData] @Date='2021-02-26 00:00:00.000', @PNR='K1VN753', @UMID=89, @LoginMID='22'
CREATE PROCEDURE [dbo].[proc_WAT_FetchCaseSectorFromFormatedData]  
   @Date NVARCHAR(100),  
   @PNR VARCHAR(200),
   @UMID INT,
   @LoginMID INT  =22
AS  
    BEGIN  
       DECLARE @SaveDateTime DATETIME = GETDATE();  
	   DECLARE @TableName VARCHAR(200)=''
	   DECLARE @InsertStr VARCHAR(MAX)=''
	   DECLARE @ColumnName VARCHAR(MAX)=''
	   DECLARE @DateColumnName VARCHAR(200)=''
	   DECLARE @PNRColumnName VARCHAR(200)=''

       BEGIN TRY     
	   SELECT @DateColumnName=DBColumnName FROM UploadDataColumnMapping WHERE  UMID=@UMID  AND FreezeStatus=0 AND DataMDBColumnName='Date'
	   SELECT @PNRColumnName=DBColumnName FROM UploadDataColumnMapping WHERE  UMID=@UMID  AND FreezeStatus=0 AND DataMDBColumnName='PNR'

	   SELECT @TableName=DataTable FROM uploadMaster WHERE UMID=@UMID
		SET @ColumnName=(SELECT  (SELECT CASE WHEN s.DataMDBColumnName='DATE' THEN 'CONVERT(VARCHAR(12), '+s.[DBColumnName]+', 106) AS '+cast(s.DBColumnName as varchar(200)) +''  ELSE  cast(s.DBColumnName as varchar(200))  END+ ', ' 
                      FROM [dbo].UploadDataColumnMapping s
					  WHERE  UMID=@UMID 
                      ORDER BY s.UploadDataCMID
                     FOR XML PATH('')))
		SET @ColumnName=(SELECT  CASE LEN(@ColumnName) WHEN 0 THEN @ColumnName ELSE LEFT(@ColumnName, LEN(@ColumnName) - 1) END )


		SET @InsertStr='
		SELECT '+@ColumnName+' FROM '+@TableName+' where '+@DateColumnName+'='''+@Date+''' AND '+@PNRColumnName+'='''+@PNR+'''
		'
		EXEC(@InsertStr)

		END TRY  
        BEGIN CATCH  
		ROLLBACK TRAN
         SELECT   ERROR_MESSAGE()
        END CATCH;                      
    END;  

	
GO

   

/****** Object:  StoredProcedure [dbo].[proc_WAT_DataMasterUpdate]    Script Date: 9/27/2021 4:02:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
  -- =============================================
-- Author:		<Mahesh Mamgain>
-- Create date: <23-August-21>
-- =============================================

--EXEC [proc_WAT_DataMasterUpdate] @FileUMID='5063',@EID='0'
CREATE PROCEDURE [dbo].[proc_WAT_DataMasterUpdate]  
   @FileUMID INT=0,  
   @EID INT  =0
AS  
    BEGIN  
       DECLARE @SaveDateTime DATETIME = GETDATE();  
	   DECLARE @UMID INT=0
	   DECLARE @DataTable VARCHAR(100)=''
	   DECLARE @DestinationColumnName VARCHAR(MAX)=''
	   DECLARE @SourceColumnName VARCHAR(MAX)=''
	   DECLARE @InsertStr VARCHAR(MAX)=''
       BEGIN TRY     
	   	
		IF OBJECT_ID('tempdb..#TempDataMaster') IS NOT NULL    
        DROP TABLE #TempDataMaster    
		
		CREATE TABLE #TempDataMaster(
			[ID] [bigint] NULL,
			[DataUMID] [int] NULL,
			[UMID] [int] NULL,
			[Date] [datetime] NULL,
			[PNR] [varchar](500) NULL,
			[Para1] [varchar](1000) NULL,
			[Para2] [varchar](1000) NULL,
			[Para3] [varchar](1000) NULL,
			[Para4] [varchar](1000) NULL,
			[Para5] [varchar](1000) NULL,
			[Para6] [varchar](1000) NULL,
			[Para7] [varchar](1000) NULL,
			[Para8] [varchar](1000) NULL,
			[Para9] [varchar](1000) NULL,
			[Para10] [varchar](1000) NULL,
			[FreezeStatus] [tinyint] default((0))
			) 

	      SELECT @UMID=UMID, @DataTable = DataTable FROM uploadMaster WHERE UMID IN (
				SELECT UMID FROM FileUploadMaster WHERE fileumid=@FileUMID) and FreezeStatus=0
		  
		SET @DestinationColumnName=(SELECT  (SELECT cast(s.DataMDBColumnName as varchar(200)) + ', ' 
                      FROM [dbo].UploadDataColumnMapping s
					  WHERE  UMID=@UMID 
                      ORDER BY s.UploadDataCMID
                     FOR XML PATH('')))
		SET @DestinationColumnName=(SELECT  CASE LEN(@DestinationColumnName) WHEN 0 THEN @DestinationColumnName ELSE LEFT(@DestinationColumnName, LEN(@DestinationColumnName) - 1) END )

		SET @SourceColumnName=(SELECT  (SELECT cast(s.DBColumnName as varchar(200)) + ', ' 
                      FROM [dbo].UploadDataColumnMapping s
					  WHERE  UMID=@UMID 
                      ORDER BY s.UploadDataCMID
                     FOR XML PATH('')))
		SET @SourceColumnName=(SELECT  CASE LEN(@SourceColumnName) WHEN 0 THEN @SourceColumnName ELSE LEFT(@SourceColumnName, LEN(@SourceColumnName) - 1) END )

		--print(@SourceColumnName)  

		IF (LEN(@DestinationColumnName)>0 AND LEN(@SourceColumnName)>0)
		BEGIN
		SET @InsertStr = '
							INSERT INTO #TempDataMaster('+ @DestinationColumnName +', ID,DataUMID,UMID,FreezeStatus)
							(SELECT '+ @SourceColumnName +', ID, DataUMID,'+CAST(@UMID AS VARCHAR(10))+', 0 FROM '+@DataTable+' WHERE DataUMID='+ CAST(@FileUMID AS VARCHAR(10)) +')
							
							SELECT *, DENSE_RANK() Over(Partition by [date],[PNR] order by ID) DataRank INTO #tempDuplicateOrder FROM #TempDataMaster

							UPDATE TD SET TD.FreezeStatus=1 FROM #tempDuplicateOrder TD
							JOIN WAT_DataMaster DM ON TD.[Date]=DM.[Date] AND TD.[PNR]=DM.[PNR] AND TD.[UMID]=DM.[UMID]
							WHERE DM.FreezeStatus=0
	
							UPDATE #tempDuplicateOrder SET FreezeStatus=1 WHERE DataRank <> 1
						
							INSERT INTO WAT_DataMaster('+ @DestinationColumnName +', ID,DataUMID,UMID,FreezeStatus,DispositionStatus,CreatedDateTime )
							(SELECT '+ @DestinationColumnName +', ID, DataUMID,'+CAST(@UMID AS VARCHAR(10))+', FreezeStatus,0,GETDATE() FROM #tempDuplicateOrder
							 WHERE  FreezeStatus=0)
						'
		END
		  
		EXEC(@InsertStr)  
		
		END TRY  
        BEGIN CATCH  
         SELECT   ERROR_MESSAGE()
        END CATCH;                      
    END;  

	
	
GO

/****** Object:  StoredProcedure [dbo].[proc_WAT_EasyJet_ActivityTrackerFlightRefundDataValidate]    Script Date: 9/27/2021 4:02:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Select * from EU_RawDataMaster            
            
            
--Exec [proc_WAT_EasyJet_ActivityTrackerDataValidate] 3682           
CREATE PROCEDURE [dbo].[proc_WAT_EasyJet_ActivityTrackerFlightRefundDataValidate] @fileUMID BIGINT            
AS            
    BEGIN            
 ---Transaction Start for the Data            
        BEGIN TRAN saveData              
  --Declaration for error message            
        DECLARE @errorNumber BIGINT            
        DECLARE @errorMessage VARCHAR(MAX)              
        SET @errorNumber = 0               
        SET @errorMessage = 'Failed to insert data in FlightRefundDataMaster on FlightRefundDataMaster following error.. ;'              
        --for any error comes in procedure error will caught in CATCH.            
        BEGIN TRY              
            BEGIN             
                DECLARE @SaveDateTime DATETIME            
                SET @SaveDateTime = GETDATE()            
                DECLARE @RowCount INT            
                DECLARE @ClientID INT            
                DECLARE @HRSystemSourcesID INT            
                DECLARE @HRSourceRoleID BIGINT            
                    
                SELECT  @ClientID = EUF.ClientID ,            
                        @HRSystemSourcesID = HRSS.HRSystemSourcesID            
                FROM    dbo.EasyJet_FileUplaodMaster EUF            
                        JOIN EmeraldHierarchies_MVC.dbo.HRSystemSources HRSS ON HRSS.ClientID = EUF.ClientID            
                WHERE   EUF.[Status] = 1            
                        AND FileUMID = @fileUMID AND EUF.ClientID=202          
                    
                IF OBJECT_ID('tempdb..#TempData') IS NOT NULL            
                    DROP TABLE #TempData            
                
                UPDATE  EasyJet_RawDataMaster            
                SET     RequestDate = CASE WHEN ISNUMERIC(RequestDate) = 1            
                                             THEN CONVERT(VARCHAR(50), CAST(CAST(RequestDate AS FLOAT) AS DATETIME)            
                                                  - 2, 121)            
                                             ELSE RequestDate            
                                        END ,            
                                 
                        CustomerEmailAddress = LTRIM(RTRIM(CustomerEmailAddress)) ,            
                        PNR = LTRIM(RTRIM(PNR))            
                 
                WHERE   FileUMID = @fileUMID            
                                
                SELECT  *            
                INTO    #TempData            
                FROM    dbo.EasyJet_RawDataMaster            
                WHERE   FileUMID = @fileUMID           
          
    UPDATE  #TempData            
                SET     Reason = ISNULL(Reason, '') + ' | Request Date is Required' ,            
                        [Status] = 2            
                WHERE   RequestDate IS NULL            
                        OR LTRIM(RTRIM(RequestDate)) = ''           
      
      
       UPDATE  #TempData        
                SET     Reason = ISNULL(Reason, '')        
            + ' | Invalid Request Date' ,        
                        [Status] = 2        
                WHERE   ISDATE(ISNULL(RequestDate, '1900-01-01')) = 0         
      
              
                UPDATE  #TempData            
                SET     Reason = ISNULL(Reason, '') + ' | Email address Required' ,            
                        [Status] = 2            
                WHERE   CustomerEmailAddress IS NULL            
                        OR LTRIM(RTRIM(CustomerEmailAddress)) = ''      
            
     UPDATE  #TempData            
                SET     Reason = ISNULL(Reason, '') + ' | Email address is invalid' ,            
                        [Status] = 2            
                WHERE   dbo.vaValidEmail(CustomerEmailAddress) = '0'      
                    
                                UPDATE  #TempData            
                SET     Reason = ISNULL(Reason, '') + ' | PNR Number Required' ,            
    [Status] = 2            
                WHERE   PNR IS NULL            
                        OR LTRIM(RTRIM(PNR)) = ''        
            
            
    UPDATE  #TempData       
                SET     Reason = ISNULL(Reason, '') + ' | PNR Number should be max only 10 digit' ,            
                        [Status] = 2            
                WHERE   LEN(PNR) > 10         
      
      
     UPDATE  #TempData            
                SET     Reason = ISNULL(Reason, '') + ' | PNR Number should be alphanumeric value' ,            
                        [Status] = 2            
               WHERE    dbo.FlightRefundpnrNumberValidate(PNR,0) = '1'      
           
           
                             
                             
          
                UPDATE  #TempData            
                SET     Status = 1            
                WHERE   Reason IS NULL            
                                  
                        UPDATE  #TempData            
                        SET     [Status] = 3            
                        WHERE   [Status] = 1            
                                            
                    END            
                                    
                UPDATE  EUD            
                SET     EUD.[Status] = TD.[Status] ,            
                        EUD.Reason = TD.Reason            
                FROM    EasyJet_RawDataMaster EUD            
                        JOIN #TempData TD ON TD.RawDMID = EUD.RawDMID            
                                
                IF ( ( SELECT   COUNT(*)            
                       FROM     #TempData            
                                 
                     ) = 0 )            
                    BEGIN            
                        UPDATE  dbo.EasyJet_FileUplaodMaster            
                        SET     [Status] = 4 ,            
                                RecordCount = ( SELECT  COUNT(*)            
                                                FROM    dbo.EasyJet_RawDataMaster            
                                                WHERE   FileUMID = @fileUMID            
                                              ) ,            
                                ValidRecordCount = ( SELECT COUNT(*)            
                                                     FROM   dbo.EasyJet_RawDataMaster            
                                                     WHERE  FileUMID = @fileUMID            
                                                            AND [Status] <> 2            
                                                   ) ,            
                                InValidRecordCount = ( SELECT COUNT(*)            
                                                       FROM   dbo.EasyJet_RawDataMaster            
                                                       WHERE  FileUMID = @fileUMID            
                                                              AND [Status] = 2            
                                                     )            
                        WHERE   FileUMID = @fileUMID                          
                    END                              
                ELSE            
                    BEGIN            
                        UPDATE  dbo.EasyJet_FileUplaodMaster            
                        SET     [Status] = 3 ,            
                                RecordCount = ( SELECT  COUNT(*)            
                                                FROM    dbo.EasyJet_RawDataMaster            
                                                WHERE   FileUMID = @fileUMID            
                                              ) ,            
                                ValidRecordCount = ( SELECT COUNT(*)            
                                                     FROM   dbo.EasyJet_RawDataMaster            
                                                     WHERE  FileUMID = @fileUMID            
                                                            AND [Status] <> 2            
                                                   ) ,            
                        InValidRecordCount = ( SELECT COUNT(*)            
                                                       FROM   dbo.EasyJet_RawDataMaster            
                                                       WHERE  FileUMID = @fileUMID            
          AND [Status] = 2            
                                                     )            
                        WHERE   FileUMID = @fileUMID            
                                   
                        DECLARE @batchGuID UNIQUEIDENTIFIER            
                        SELECT  @batchGuID = NEWID()                                
                                
                        SELECT  ROW_NUMBER() OVER ( ORDER BY RawDMID ) UniID ,            
                                RawDMID ,            
                                RequestDate ,            
                                CustomerEmailAddress ,            
                                @batchGuID AS BatchGUID ,            
                                PNR ,            
                                Language ,            
                                PNRType            
                                          
                        INTO    #Temp_FinalData            
                        FROM    dbo.EasyJet_RawDataMaster RDM            
                               JOIN EasyJet_FileUplaodMaster FUM ON RDM.FileUMID = FUM.FileUMID            
                                                      WHERE   RDM.Status = 3            
                                          
                                AND FUM.Uploadtype = 1            
                                AND RDM.FileUMID = @fileUMID         
              
              
                            
                        DECLARE @Count INT = 1            
        
                        WHILE @Count <= ( SELECT    COUNT(*)            
                                          FROM      #Temp_FinalData            
                                        )            
                            BEGIN                
                                DECLARE @RequestDate datetime          
                                DECLARE @CustomerEmailAddress VARCHAR(500)            
                                DECLARE @PNR VARCHAR(500)             
                                DECLARE @Language VARCHAR(500)            
                                DECLARE @PNRType VARCHAR(100)            
                                         
                                DECLARE @rawDMID BIGINT            
                                SELECT  @RequestDate = ISNULL(RequestDate, '') ,            
                                        @CustomerEmailAddress = ISNULL(CustomerEmailAddress, '') ,              
                                        @rawDMID = RawDMID ,            
                                        @PNR = PNR ,            
                                        @Language = Language ,            
                                        @PNRType = PNRType          
                                                 
                                FROM    #Temp_FinalData            
                                WHERE   UniID = @Count            
        
                                       INSERT  INTO FlightRefundDataMaster        
                                        VALUES  ( @RequestDate,          
                                                  @CustomerEmailAddress,        
                                                    @PNR,        
                                                    @Language,        
                                                   @PNRType,        
                                                    0)        
                                                                  
                  
                            
                                          
                                UPDATE  dbo.EasyJet_RawDataMaster            
                                SET                 
                                        Status = 4            
                                WHERE   RawDMID = @rawDMID     
                  
                                SET @Count = @Count + 1                
                            END                              
                                       
                        UPDATE  dbo.EasyJet_FileUplaodMaster            
                        SET     [Status] = 4            
                        WHERE   FileUMID = @fileUMID                            
                                       
               
            
                COMMIT TRAN saveData            
            END             
          
             
        END TRY              
        ---Catch statement for any error occure and roleback the transaction.            
        BEGIN CATCH              
            ROLLBACK TRAN saveData               
            SET @errorNumber = ERROR_NUMBER()              
            SET @errorMessage = @errorMessage + '  ; System Error Message :-'            
                + ERROR_MESSAGE() + CAST(@errorNumber AS VARCHAR(100))              
            RAISERROR(@errorMessage,16,1)              
            --RETURN @errorNumber              
            SELECT  @errorNumber AS errorNumber ,            
                    0 AS customiseRDID ,            
                    @errorMessage AS errorMessage            
        END CATCH            
    END
GO

/****** Object:  StoredProcedure [dbo].[proc_Admin_ActivityTrackerFlightRefundDataFileUpload]    Script Date: 9/27/2021 4:02:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

      
CREATE PROCEDURE [dbo].[proc_Admin_ActivityTrackerFlightRefundDataFileUpload]                
    @ClientID INT ,                
    @FilePath VARCHAR(200) ,                
    @FileName VARCHAR(100) ,                
    @FilePathSystem VARCHAR(200) ,                
    @ActualFileName VARCHAR(100) ,                
    @Status TINYINT ,                
    @UploadData NVARCHAR(MAX) ,                
    @UploadType TINYINT ,                
    @CreatedBy VARCHAR(100) ,                
    @HostName VARCHAR(100) ,  
    @ErrorNumber BIGINT OUTPUT                
AS                
    BEGIN                 
    ---Transaction Start for the Data                
        BEGIN TRAN saveData                 
            --Declaration for error message                
                
        DECLARE @errorMessage VARCHAR(MAX)                
        SET @errorMessage = 'Failed to save data of fileuploaded following error.. ;'                
                  
        BEGIN TRY                
            BEGIN                 
                DECLARE @FileUMID INT = 0                
                DECLARE @STR VARCHAR(MAX)                
                DECLARE @ClientColumn VARCHAR(2000)                
                DECLARE @InsertclientColumn VARCHAR(2000)                
                
             INSERT  INTO dbo.EasyJet_FileUplaodMaster                
                        ( ClientID ,                
                          FilePathSystem ,                
                          FileName ,                
                          ActualFileName ,                
                          Status ,                
                          Uploadtype ,                
                          CreatedDateTime ,                
                          CreatedBy ,                
                          HostName,  
						  FileQueueType  
                        )                
                VALUES  ( @ClientID , -- ClientID - int                    
                          @FilePathSystem , -- FilePathSystem - varchar(100)                    
                          @FileName , -- FileName - varchar(200)                    
                          @ActualFileName , -- ActualFileName - varchar(100)                    
                          @Status , -- Status - tinyint                    
                          @UploadType , -- Uploadtype - tinyint                    
                          GETDATE() , -- CreatedDateTime - datetime                    
                          @CreatedBy , -- CreatedBy - varchar(100)                    
                          @HostName,  -- HostName - varchar(100)   
						  @UploadType  -- HostName - varchar(100)   
          
                        ) SET @FileUMID = SCOPE_IDENTITY();                
                
                PRINT(@FileUMID)    
          
                DECLARE @saveDatetime DATETIME                
                SET @saveDatetime = GETDATE()                
                DECLARE @ptrHandle INT                    
                DECLARE @parameterXML XML                
                SET @ptrHandle = 0                 
                SET @parameterXML = @UploadData                
                SET NOCOUNT ON;                    
                EXEC sp_xml_preparedocument @ptrHandle OUTPUT, @parameterXML                    
                    
                SELECT  *                
                INTO    #tmpRawdata                
                FROM    OPENXML(@ptrHandle, '/NewDataSet/UploadData', 2)                    
				  WITH           
				   ([RequestDate] [VARCHAR] (500) ,                
					[CustomerEmailAddress] [VARCHAR] (500) ,                
					[PNR] [VARCHAR] (500) ,                
					[Language] [VARCHAR] (500) ,                
					[PNRType] [VARCHAR] (500)  
					)            
                   
				   
				        INSERT  INTO dbo.EasyJet_RawDataMaster                
                        ( FileUMID ,                
						  RequestDate ,                
                          CustomerEmailAddress ,                
                          PNR ,                
                          Language ,                
                          PNRType                                          
						)                
                        SELECT @FileUMID ,                
                                RequestDate ,                
                                CustomerEmailAddress ,                
                                PNR ,                
                                [Language] ,                
                                PNRType                                     
                        FROM    #tmpRawdata            
                
                          SET @ErrorNumber = 1                        
             END                
               COMMIT TRAN saveData             
               Exec [proc_WAT_EasyJet_ActivityTrackerFlightRefundDataValidate] @FileUMID                
  
                            
        END TRY                          
        BEGIN CATCH                   
            ROLLBACK TRAN saveData                         
            SET @ErrorNumber = 2                 
            SET @errorNumber = ERROR_NUMBER()                
            SET @errorMessage = @errorMessage + '  ; System Error Message :-'                
                + ERROR_MESSAGE() + CAST(@errorNumber AS VARCHAR(100))                
            RAISERROR(@errorMessage,16,1)                
        END CATCH                
    END 
GO

/****** Object:  StoredProcedure [dbo].[proc_Easyjet_GetActivityTracketDataList]    Script Date: 9/27/2021 4:02:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

          
CREATE PROCEDURE [dbo].[proc_Easyjet_GetActivityTracketDataList]          
    (          
      @LoginMID BIGINT ,          
      @AccessType INT          
    )          
AS           
    BEGIN          
        IF (@AccessType = 6)        
            BEGIN             
                SELECT  row_number() OVER ( ORDER BY ( SELECT ''          
                                                     ) ) AS RowId ,          
                        CL.[ClientID] ,          
                        CL.[ClientName] ,          
                        EUF.[FileUMID] ,          
                        EUF.[FilePath] ,          
                        EUF.[FileName] ,          
                        EUF.[ActualFileName] ,          
                        EUS.[Status] ,          
                        EUF.[RecordCount] ,          
                        EUF.[ValidRecordCount] ,          
                        EUF.[InvalidRecordCount] ,          
                        EUF.[CreatedDateTime] ,          
                        EUF.[CreatedBy] ,          
                        EUF.[UpdatedDateTime] ,          
                        EUF.[UpdatedBy] ,          
                        EUF.[HostName] ,          
                        LM.[EmployeeName]          
                FROM    dbo.EasyJet_FileUplaodMaster EUF          
                        JOIN EmeraldHierarchies_MVC.DBO.Clients CL ON CL.ClientID = EUF.ClientID          
                        JOIN dbo.EU_StatusMaster EUS ON EUS.StatusID = EUF.[Status]          
                        JOIN dbo.LoginMaster LM ON LM.LoginMID = EUF.CreatedBy          
                WHERE   EUF.CreatedDateTime >= DATEADD(day, -7, GETDATE())         
                 ORDER BY EUF.CreatedDateTime DESC         
            END            
        ELSE           
            BEGIN            
                SELECT  row_number() OVER ( ORDER BY ( SELECT ''          
                                                     ) ) AS RowId ,          
                        --'Status' AS [Status] ,          
                        CL.[ClientID] ,          
                        CL.[ClientName] ,          
                        EUF.[FileUMID] ,          
                        EUF.[FilePath] ,          
                        EUF.[FileName] ,          
                        EUF.[ActualFileName] ,          
                        EUS.[Status] ,          
                        EUF.[RecordCount] ,          
                        EUF.[ValidRecordCount] ,          
                        EUF.[InvalidRecordCount] ,          
                        EUF.[CreatedDateTime] ,          
                        EUF.[CreatedBy] ,          
                        EUF.[UpdatedDateTime] ,          
                        EUF.[UpdatedBy] ,          
                        EUF.[HostName] ,          
                        LM.[EmployeeName]          
                FROM    dbo.EasyJet_FileUplaodMaster EUF          
                        JOIN EmeraldHierarchies_MVC.DBO.Clients CL ON CL.ClientID = EUF.ClientID          
                        JOIN dbo.EU_StatusMaster EUS ON EUS.StatusID = EUF.[Status]                                  
                        JOIN dbo.LoginMaster LM ON LM.LoginMID = EUF.CreatedBy          
                WHERE   EUF.CreatedDateTime >= DATEADD(day, -7, GETDATE())          
                        AND EUF.CreatedBy = @LoginMID        
                                
                         ORDER BY EUF.CreatedDateTime DESC      
            END          
    END 
GO





