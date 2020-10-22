create proc DeleteEvent(@eventId int)
as
    delete from Event 
	where Id = @eventId