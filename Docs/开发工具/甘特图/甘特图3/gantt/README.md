#jQuery.Gantt

link
http://taitems.github.io/jQuery.Gantt/

##Gantt Configuration 
___
```javascript
$(".selector").gantt({
	source: "ajax/data.json",
	scale: "weeks",
	minScale: "weeks",
	maxScale: "months",
	onItemClick: function(data) {
		alert("Item clicked - show some details");
	},
	onAddClick: function(dt, rowId) {
		alert("Empty space clicked - add an item!");
	},
	onRender: function() {
		console.log("chart rendered");
	}
});
```

|Parameter     |Default       |	Accepts Type |
|--------------|--------------|--------------|
|source  |  	[] | 	Array, String (url) |
|itemsPerPage 	|7 	|Number |
|months 	|["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"] 	|Array|
|dow 	|["S", "M", "T", "W", "T", "F", "S"] |	Array|
|navigate 	|"buttons"| 	String ("buttons","scroll")|
|scale 	"days" 	|String|
|maxScale 	|"months"| 	String|
|minScale 	|"hours"| 	String|
|waitText 	|"Please Wait..." 	|String|
|onItemClick 	|function (data) { return; }| 	a JS Function that gets called when clicking on a Gantt-Item.
The parameter passed to the function is the dataObj of the item|
|onAddClick 	|function (dt, rowId) { return; }| 	a JS Function that gets called when clicking on a Gantt-Item.The parameter passed to the function is the DateTime in ms for the clicked Cell, and the ID if the source object (row)|
|onRender 	|function () { return; } 	|a JS Function called whenever the chart is (re)rendered|
|useCookie 	|false 	|indicates if cookies should be used to track the chart's state (scale, scrollposition) between postpacks.jquery.cookie.js needs to be referenced for this to work|
|scrollToToday 	|true 	|Boolean|

## Source Configuration 
____
```javascript
source: [{
	name: "Example",
	desc: "Lorem ipsum dolor sit amet.",
	values: [ ... ]
}]

```

|Parameter     |Default       |	Accepts Type | Meaning|
|--------------|--------------|--------------|--------|
|name 	|null 	|String 	|Bold value in the left-most column of the gantt row.|
|desc 	|null 	|String 	|Secondary value in the gantt row.|
|values 	|null |	Array |	Collection of date ranges for gantt items. See next table. |

## Value Configuration 
______
```javascript
values: [{
	to: "/Date(1328832000000)/",
	from: "/Date(1333411200000)/",
	desc: "Something",
	label: "Example Value",
	customClass: "ganttRed",
	dataObj: foo.bar[i]
}]
```
|Parameter     |Default       |	Accepts Type |
|--------------|--------------|--------------|
|to 	|String (Date) 	|-
|from 	|String (Date) 	|-
|desc 	|String 	|Text that appears on hover, I think?
|label 	|String 	|Appears on the gantt item.
|customClass 	|String 	|Custom class to be applied to the gantt item.
|dataObj 	|All 	|A data object that is applied directly to the gantt item. 