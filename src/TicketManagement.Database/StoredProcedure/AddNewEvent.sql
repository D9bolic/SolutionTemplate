create proc AddNewEvent(
	@name nvarchar(120),
	@description nvarchar(Max),
	@startDt datetime,
	@finishDt datetime,
	@layoutId int,
	@price decimal,
	@state int,
	@eventId int out
	)
as
    -- add new event
    insert into Event (Name, Description, StartDateTime, FinishDateTime, LayoutId)
    values(@name, @description, @startDt, @finishDt, @layoutId)

    -- add get event id
    set @eventId = SCOPE_IDENTITY()

	-- declare vars
	declare @areaId int, @eventAreaId int
	set @areaId = ( select min(Id)
					from Area 
					where LayoutId = @layoutId
				  )

	while @areaId is not null
	begin 
	  -- add event area
	  insert into EventArea(EventId, Description, CoordX, CoordY, Price)
        select @eventId, Description, CoordX, CoordY, @price
	    from Area
	    where Id = @areaId

	  -- get event area id
	  set @eventAreaId = SCOPE_IDENTITY()
	
	  -- add event seats for current event area
      insert into EventSeat(EventAreaId, Row, Number, State)
        select @eventAreaId, Row, Number, @state
	    from Seat
	    where AreaId = @areaId

	  -- get next area id
	  set @areaId = (select min(Id)
					 from Area 
					 where LayoutId = @layoutId and Id > @areaId
				    )
	end