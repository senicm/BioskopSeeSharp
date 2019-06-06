Projekat .NET - SeeSharp bioskop

Aplikacija je radjena u Visual Studiju, a za bazu podataka koriscen je SQL Server Management Studio. 
Za izradu aplikacije koriscen je ASP.NET framework, kao i HTML/CSS/Javascript. SQL (Structured Query Language).

U SQL bazi podataka filmoviDBnew1 nalaze se 4 tabele:
- filmovi (kolone ID, nazivfilma, opisfilma, nazivdatoteke)
- korisnici (kolone username, password, useremail)
- rezervacije (kolone ID, filmdansat, kreatoruser, indexirezervsed)
- sala (kolone ID, filmdansat, stanjesale)

Sve koriscene slike se cuvaju u folderu projekta Images.
Pored Template.master, postoji 5 stranica u .aspx formatu:
- Default
- Account
- Filmovi
- Rezervacija
- Galerija

Da bi se videle sve stranice u projektu, korisnik mora biti registrovan, a potom i ulogovan.
Kada je korisnik ulogovan, moze da rezervise sedista za odredjeni film koji se prikazuje u odredjeno vreme.
