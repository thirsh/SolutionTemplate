SET IDENTITY_INSERT [dbo].[Widgets] ON 

MERGE INTO [dbo].[Widgets] AS Target 
USING (VALUES 
  (1, N'WizBang', 1), 
  (2, N'Gizmo', 1) 
) 
AS Source ([Id], [Name], [Active]) 
ON Target.[Id] = Source.[Id] 

-- update matched rows 
WHEN MATCHED THEN 
UPDATE SET 
[Name] = Source.[Name], 
[Active] = Source.[Active] 

-- insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id], [Name], [Active]) 
VALUES ([Id], [Name], [Active]) 

-- delete rows that are in the target but not the source 
WHEN NOT MATCHED BY SOURCE THEN 
DELETE;

SET IDENTITY_INSERT [dbo].[Widgets] OFF 
