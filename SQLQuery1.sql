CREATE DATABASE filmoviDBnew
USE filmoviDBnew
GO
CREATE TABLE filmovi(
	ID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	NazivFilma nvarchar(255) NULL,
	OpisFilma nvarchar(max) NULL,
	NazivDatoteke nvarchar(255) NULL
	)
CREATE TABLE korisnici(
	username nvarchar(255) NOT NULL PRIMARY KEY,
	[password] nvarchar(255) NOT NULL,
	useremail nvarchar(255) NULL,
	)
CREATE TABLE sala(
	ID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FilmDanSat nvarchar(255) NULL,
	StanjeSale nvarchar(255) NULL,
	)
CREATE TABLE rezervacije(
	ID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FilmDanSat nvarchar(255) NULL,
	KreatorUser nvarchar(255) NULL,
	IndexiRezervSed nvarchar(max) NULL,
	)
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX username ON korisnici
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX FilmDanSat ON sala
(
	[FilmDanSat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE sala ADD  DEFAULT ('OOOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO') FOR [StanjeSale]
GO
ALTER TABLE korisnici  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$korisnici$password$disallow_zero_length] CHECK  ((len(password)>(0)))
GO
ALTER TABLE korisnici  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$korisnici$username$disallow_zero_length] CHECK  ((len(username)>(0)))
ALTER DATABASE filmoviDBnew SET  READ_WRITE 
GO
INSERT INTO filmovi VALUES ('Black Swan', 'A committed dancer struggles to maintain her sanity after winning the lead role in a production of Tchaikovsky''s "Swan Lake"', 'black swan poster'),
('Spirited Away', 'During her family''s move to the suburbs, a sullen 10-year-old girl wanders into a world ruled by gods, witches, and spirits, and where humans are changed into beasts.', 'spirited away poster'),
('Suspiria', 'An American newcomer to a prestigious German ballet academy comes to realize that the school is a front for something sinister amid a series of grisly murders.', 'suspiria poster'),
('Requiem For A Dream', 'The drug-induced utopias of four Coney Island people are shattered when their addictions run deep.', 'requiem poster'),
('Lost Highway', 'After a bizarre encounter at a party, a jazz saxophonist is framed for the murder of his wife and sent to prison, where he inexplicably morphs into a young mechanic and begins leading a new life.', 'lh poster')
INSERT INTO korisnici VALUES ('admin', 'admin', 'admin@admin.com'),
('regular', 'regular', 'regular@regular.com')
INSERT INTO sala VALUES ('1+2+15', 'OOOOOOOOXXXOOOOOO|OOOOOOOOXOOOOOOOO|OOOOOOOOXOOOOOOOO|OOOOOOOOXOOOOOOOO|OOOOOOOOOOOOOOOOO'),
('2+1+18', 'XXOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO'),
('3+3+21', 'OOOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO|OOOOOOOOOOOOOOOOO')
