SET IDENTITY_INSERT [dbo].[Doodads] ON 

MERGE INTO [dbo].[Doodads] AS Target 
USING (VALUES 
  (1, 1, N'Sprocket', 1), 
  (2, 1, N'Spring', 1) ,
  (3, 2, N'Gyro', 1), 
  (4, 2, N'Gear', 1),
  (5, 2, N'Cog', 1)
)
AS Source ([Id], [WidgetId], [Name], [Active]) 
ON Target.[Id] = Source.[Id] 

-- update matched rows 
WHEN MATCHED THEN 
UPDATE SET 
[WidgetId] = Source.[WidgetId],
[Name] = Source.[Name], 
[Active] = Source.[Active] 

-- insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id], [WidgetId], [Name], [Active]) 
VALUES ([Id], [WidgetId], [Name], [Active]) 

-- delete rows that are in the target but not the source 
WHEN NOT MATCHED BY SOURCE THEN 
DELETE;

SET IDENTITY_INSERT [dbo].[Doodads] OFF 
