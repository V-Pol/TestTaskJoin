CREATE TABLE [dbo].[FrequencyReports] (
    [FrequencyReportId] INT           IDENTITY,
    [FrequencyText]     NVARCHAR (70)  NOT NULL
    PRIMARY KEY  CLUSTERED ([FrequencyReportId] ASC)
);

INSERT INTO [dbo].[FrequencyReports] ([FrequencyText]) 
VALUES (N'ежемесячно'),
(N'ежеквартально'),
(N'ежегодно');

CREATE TABLE [dbo].[Reports] (
    [ReportId] INT           IDENTITY,
    [ReportName]     NVARCHAR (150)  NOT NULL,
    [FrequencyReportId] INT NOT NULL, 
    PRIMARY KEY  CLUSTERED ([ReportId] ASC),
	FOREIGN KEY (FrequencyReportId) REFERENCES FrequencyReports (FrequencyReportId)
);

INSERT INTO [dbo].[Reports] ([ReportName], [FrequencyReportId]) 
VALUES (N'Декларация по НДС', 2),
(N'Декларация по налогу на прибыль',3),
(N'4-ФСС', 2),
(N'Расчет по страховым взносам', 2),
(N'Отчетность по НДФЛ', 2),
(N'СЗВ-М', 1),
(N'СЗВ-СТАЖ', 3),
(N'СЗВ-ТД', 1);