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
    
    var marker = new google.maps.Marker({
        position: center,       
        map: map
    });
    function addMarker(feature)
    {
        var marker = new google.maps.Marker({
            position: feature.position,
            map: map
        })
    }
    var features = [
        {
            position: new google.maps.LatLng(45, -122)
        }
    ]
    for (var i = 0, feature; feature = features[i]; i++) {
        addMarker(feature);
    }
}
