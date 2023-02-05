
var locations = [];

// Obtain all coordinate data from html

$(".coordinates").each(function () {

	var name = $(".name", this).text().trim();
	var longitude = $(".longitude", this).text().trim();
	var latitude = $(".latitude", this).text().trim();

	console.log(name);

	// Create a point data structure to hold the values.
	var point = {
		"name": name,
		"latitude": latitude,
		"longitude": longitude
	};

	// Push them all into an array.
	locations.push(point);

});

// Create the map and overlay attributes focused on Melbourne location
var map = L.map('map').setView([-37.87682300, 145.04583700], 13);

	var tiles = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
		maxZoom: 19,
		attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
	}).addTo(map);

// For each gym add a marker to the map
for (i = 0; i < locations.length; i++) {
	var marker = L.marker([locations[i].longitude, locations[i].latitude]).addTo(map)
		.bindPopup('<b>' + locations[i].name + '</b>').openPopup();
}

	function onMapClick(e) {
		popup
			.setLatLng(e.latlng)
			.setContent('You clicked the map at ' + e.latlng.toString())
			.openOn(map);
	}

	map.on('click', onMapClick);
