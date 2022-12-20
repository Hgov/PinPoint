/*
loglamay� veritaban�na kaydetmek i�in yaz�ld�
*/
CREATE PROCEDURE [dbo].[SP_Nlog_Insert] (
@machineName nvarchar(200),
@logged datetime,
@level varchar(5),
@message nvarchar(max),
@logger nvarchar(300),
@properties nvarchar(max),
@callsite nvarchar(300),
@exception nvarchar(max)
) AS
BEGIN
INSERT INTO [dbo].[NLogs] (
[MachineName],
[Logged],
[Level],
[Message],
[Logger],
[Properties],
[Callsite],
[Exception]
) VALUES (
@machineName,
@logged,
@level,
@message,
@logger,
@properties,
@callsite,
@exception
)
END