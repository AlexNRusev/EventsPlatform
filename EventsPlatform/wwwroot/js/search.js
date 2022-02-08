$(document).ready(function () {
    document.getElementById('search-select').onchange = function () {
        var index = this.selectedIndex;
        var value = this.children[index].value;//.innerHTML.trim();
        var searchField = document.getElementById('search-field');
        if (value == 1) {
            searchField.placeholder = "Въведи град";
        } else if (value == 2) {
            searchField.placeholder = "Въведи име";
        } else {
            searchField.placeholder = "";
        }
    }
});

function search() {

    var searchField = document.getElementById('search-field');
    var searchFieldValidation = document.getElementById('search-field-validation');
    var letterNumber = /^[0-9a-zA-Zа-яА-Я]+$/;
    var selectedFilter = document.getElementById('search-select');
    var selectFieldValidation = document.getElementById('select-field-validation');
    var value = selectedFilter.children[selectedFilter.selectedIndex].value;

    selectFieldValidation.innerHTML = "";
    searchFieldValidation.innerHTML = "";

    if (value != 1 && value != 2) {
        selectFieldValidation.innerHTML = "Избери критерии за търсене";
        return;
    }
    if (searchField.value.trim() == "") {
        searchFieldValidation.innerHTML = "Полето не трябва да бъде празно";
        return;
        //console.log();
    }
    if (!searchField.value.match(letterNumber)) {
        searchFieldValidation.innerHTML = "Полето може да съдържа единствено букви и цифри";
        return;
    }
    $('#found-events').empty();
    if (value == 1) { //Търсене по град
        let data = {
            town: searchField.value
        };

        $.ajax(
            {
                type: "Get",
                url: "/Events/SearchByTown",
                data: data,
                success: function (response) {
                    generateListOfEvents(response);
                }
            });
    } else if (value == 2) { // Търсене по име
        let data = {
            name: searchField.value
        };

        $.ajax(
            {
                type: "Get",
                url: "/Events/SearchByName",
                data: data,
                success: function (response) {
                    generateListOfEvents(response);
                }
            });
    }

    

}

function generateListOfEvents(events) {
    if (events.length == 0) return;

    var list = '<div class="list-group">';
    for (var i = 0; i < events.length; i++) {
        list += `<a href="/Events/Details/${events[i].id}" class="list-group-item list-group-item-action flex-column align-items-start">
    <div class="d-flex w-100 justify-content-between">
      <h5 class="mb-1">${events[i].name}</h5>
      <small>${convertDate(events[i].eventStartDate)} - ${convertDate(events[i].eventEndDate)}</small>
    </div>
    <p class="mb-1">Описание: ${events[i].details}</p>
    <small>Брой места: ${events[i].maxNumberOfParticipants}</small>
  </a>`
    }
    list += '</div>'

    document.getElementById("found-events").innerHTML = list;
}

function convertDate(datetime) {
    var parts = datetime.split('T');
    return parts[0] + " " + parts[1];
}



