$(document).ready(function () {
    alert("DOpeness!");
    var myLatlng = new google.maps.LatLng(-25.363882, 131.044922);
    var map;
    function initMap() {
        map = new google.maps.Map(document.getElementById('map'), {
            zoom: 16,
            center: new google.maps.LatLng(-33.91722, 151.23064),
            mapTypeId: 'roadmap'
        });

        var iconBase = 'https://maps.google.com/mapfiles/kml/shapes/';
        var icons = {
            parking: {
                icon: iconBase + 'parking_lot_maps.png'
            },
            library: {
                icon: iconBase + 'library_maps.png'
            },
            info: {
                icon: iconBase + 'info-i_maps.png'
            }
        };

        function addMarker(feature) {
            var marker = new google.maps.Marker({
                position: feature.position,
                icon: icons[feature.type].icon,
                map: map
            });
        }
    }
});