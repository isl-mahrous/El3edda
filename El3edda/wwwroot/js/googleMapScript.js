let lat;
let lng;
let marker;

async function DisplayGoogleMap() {
    await getLocation(() => { });


    //Set the Latitude and Longitude of the Map
    var address = new google.maps.LatLng(lat, lng);

    //Create Options or set different Characteristics of Google Map
    var mapOptions = {
        center: address,
        zoom: 13,
        minZoom: 13,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
    };

    //Display the Google map in the div control with the defined Options
    var map = new google.maps.Map(
        document.getElementById("map-canvas"),
        mapOptions
    );

    //Set Marker on the Map
    marker = new google.maps.Marker({
        position: address,
        animation: google.maps.Animation.BOUNCE,
        draggable: true,
    });

    marker.setMap(map);
    google.maps.event.addListener(marker, "dragend", function () {
        lat = marker.getPosition().lat();
        lng = marker.getPosition().lng();
        getLocationAddress(lat, lng);
    });
}

function getLocation() {
    if (navigator.geolocation)
        return new Promise((resolve, reject) =>
            navigator.geolocation.getCurrentPosition((position) => {
                lat = position.coords.latitude;
                lng = position.coords.longitude;
                resolve();
            })
        );
}

function getPosition(position) {
    lat = position.coords.latitude;
    lng = position.coords.longitude;
}

google.maps.event.addDomListener(window, "load", DisplayGoogleMap);

var getLocationAddress = function (lat, lng) {
    $.ajax({
        url: "https://nominatim.openstreetmap.org/reverse",
        data: {
            lat: lat,
            lon: lng,
            format: "json",
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("User-Agent", "com.form.parking.violation v0.5");
        },
        dataType: "json",
        type: "GET",
        async: true,
        crossDomain: true,
    })
        .done(function (res) {
            state = res.address.hasOwnProperty('state') ? res.address.state : "";
            neighbourhood = res.address.hasOwnProperty('neighbourhood') ? res.address.neighbourhood : "";
            city = res.address.hasOwnProperty('city') ? res.address.city : "";
            street = res.address.hasOwnProperty('street') ? res.address.street : "";

            $("#ShippingAddress_State").val(state);
            $("#ShippingAddress_City").val(city);
            $("#ShippingAddress_Street").val(street);
            $("#ShippingAddress_Neighbourhood").val(neighbourhood);
        })
        .fail(function (error) {
            console.error(error);
        });
};