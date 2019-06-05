function otvoriSalu(idFilma) {

    var danSelect = document.getElementById("listaDana" + idFilma);

    var odabraniDan = danSelect.options[danSelect.selectedIndex].value;

    var satSelect = document.getElementById("listaVremena" + idFilma);

    var odabraniSat = satSelect.options[satSelect.selectedIndex].value;

    document.location.href = "Rezervacija.aspx?film=" + idFilma + "&dan=" + odabraniDan + "&sat=" + odabraniSat;
}

function spremiRezervaciju(idFilma, odabraniDan, odabraniSat, rezervacijaId) {
    var odabranaSedista = "";

    var pregledSedistaChildren = document.getElementById("pregledSedista").children;

    for (var i = 0; i < pregledSedistaChildren.length; i++) {
        if (pregledSedistaChildren[i].hasAttribute("name")) {
            if (pregledSedistaChildren[i].checked && !pregledSedistaChildren[i].disabled) {
                odabranaSedista += pregledSedistaChildren[i].name.substring(8) + ",";
            }
        }
    }

    if (odabranaSedista.length > 0) {
        odabranaSedista = odabranaSedista.substring(0, odabranaSedista.length - 1);
    }

    if (rezervacijaId == null) {
        document.location.href = "Rezervacija.aspx?film=" + idFilma + "&dan=" + odabraniDan + "&sat=" + odabraniSat + "&sed=" + odabranaSedista;
    } else {
        document.location.href = "Rezervacija.aspx?film=" + idFilma + "&dan=" + odabraniDan + "&sat=" + odabraniSat + "&sed=" + odabranaSedista + "&rez=" + rezervacijaId;
    }
    
}