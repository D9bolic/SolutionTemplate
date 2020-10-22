--- Venue
insert into dbo.Venue (Description, Address, Phone)
values ('First venue', 'venue address 1', '123456789001')

--- Layout
insert into dbo.Layout (VenueId, Description)
values (1, 'layout 1'),
 (1, 'layout 2')

--- Area
insert into dbo.Area (LayoutId, Description, CoordX, CoordY)
values (1, 'First area of layout 1', 1, 1),
(1, 'Second area of layout 1', 1, 5),
(2, 'First area of layout 2', 1, 2),
(2, 'Second area of layout 2', 4, 3)

--- Seat
insert into dbo.Seat (AreaId, Row, Number)
values (1, 1, 1),
(1, 1, 2),
(1, 1, 3),
(1, 2, 2),
(1, 2, 1),
(2, 1, 1),
(2, 1, 2),
(2, 1, 3),
(2, 2, 2),
(2, 2, 1),
(3, 1, 1),
(3, 1, 2),
(3, 1, 3),
(3, 2, 2),
(3, 2, 1),
(4, 1, 1),
(4, 1, 2),
(4, 1, 3),
(4, 2, 2),
(4, 2, 1)

-- Event
insert into dbo.Event(LayoutId, Name, Description, StartDateTime, FinishDateTime)
values (1, 'Event 1 of layout 1', 'event 1', '2020-11-15 15:00:00', '2020-11-15 22:45:00')

--- Area
insert into dbo.EventArea (EventId, Description, CoordX, CoordY, price)
values (1, 'First area of layout 1', 1, 1, 430),
(1, 'Second area of layout 1', 1, 5, 250)

--- Seat
insert into dbo.EventSeat (EventAreaId, Row, Number, State)
values (1, 1, 1, 1),
(1, 1, 2, 2),
(1, 1, 3, 2),
(1, 2, 2, 2),
(1, 2, 1, 2),
(2, 1, 1, 1),
(2, 1, 2, 1),
(2, 1, 3, 3),
(2, 2, 2, 2),
(2, 2, 1, 2)