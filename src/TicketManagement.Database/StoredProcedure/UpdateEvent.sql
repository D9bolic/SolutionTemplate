create proc UpdateEvent(
	@eventId int, 
	@name nvarchar(120), 
	@description nvarchar(Max), 
	@startDt datetime, 
	@finishDt datetime
	)
as
    update Event 
	set Name = @name, Description = @description, StartDateTime = @startDt, FinishDateTime = @finishDt
	where Id = @eventId