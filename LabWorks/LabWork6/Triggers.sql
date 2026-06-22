-- Task 1
CREATE TRIGGER TrSaveEmail
    ON Visitor
    AFTER UPDATE
    AS
		IF UPDATE(Email)
			INSERT INTO VisitorEmail(VisitorId, OldEmail)
			SELECT VisitorId, Email
			FROM deleted;

-- Task 2
CREATE TRIGGER TrDeleteFilm
	ON Film
	INSTEAD OF DELETE
	AS
		UPDATE Film
		SET IsDeleted = 1
		WHERE FilmId IN(SELECT FilmId
						FROM deleted);

-- Task 3
CREATE TRIGGER TrSaveVisitor
    ON Visitor
    AFTER DELETE
    AS
		INSERT INTO DeletedVisitor(VisitorId, Phone, [Name], BirthDate, Email)
		SELECT VisitorId, Phone, [Name], BirthDate, Email
		FROM deleted;

-- Task 4
ALTER TRIGGER TrAddSessionPrice
ON Session
INSTEAD OF INSERT
AS
BEGIN
	DECLARE @newPrice decimal(4,0)
	SET @newPrice=(SELECT Price
				   FROM inserted);
	IF (@newPrice<100)
		INSERT INTO [Session](FilmId, HallId, Price, StartDate, IsFilm3d)
		SELECT FilmId, HallId, 100, StartDate, IsFilm3d
		FROM inserted;
END;

-- Task 5
CREATE TRIGGER TrAddTicket
ON Ticket
INSTEAD OF INSERT
AS
BEGIN
	DECLARE @ticketRow tinyint
	DECLARE @ticketSeat tinyint
	DECLARE @maxRow tinyint
	DECLARE @maxSeat tinyint

	SELECT @ticketRow = i.[Row], @ticketSeat = i.Seat
	FROM inserted i;

	SELECT @maxRow = h.RowsCount, @maxSeat = h.SeatsCount
	FROM CinemaHall h 
		JOIN [Session] s ON s.HallId = h.HallId
		JOIN Ticket t ON t.SessionId = s.SessionId
	IF (@ticketRow > @maxRow OR @ticketSeat > @maxSeat)
		THROW 50000, 'Íîìåð ðÿäà èëè ìåñòà âûøå äîïóñòèìîãî', 1
END;

--6th Task
