$(document).ready(function () {
    
    initMap(45.5230622, -122.6764816);
    $('#locationSubmit').submit(function (event) {
        event.preventDefault();
        var mapLocation = $("input[name=mapLocation]").val()
        console.log(mapLocation);
        $.ajax({
            url: "/Home/DrawMap/",         
            type: 'POST',      
            data: $(this).serialize(),
            datatype: 'json',
            success: function (result) {
                console.log(result);
                var stringArray = result.split(",");
                var latitude = parseFloat(stringArray[0]);
                var longitude = parseFloat(stringArray[1]);
                initMap(latitude, longitude);
            }
        }).error(function () { alert("whaaaaaaaat");});
    });
});

var initMap = function (latitude, longitude) {
    console.log(latitude, longitude);
    var center = { lat: latitude, lng: longitude };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 8,
        center: center,
    });
    map.SetMapTypeId('hybrid');
    var marker = new google.maps.Marker({
        position: center,
        
        map: map
    });
}
