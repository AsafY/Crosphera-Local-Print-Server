# Crosphera-Local-Print-Server

# Enable direct printing from browser on windows machine 

# test if print server is up
# http://127.0.0.1:17080/?op=ping
# list availbale printers
# http://127.0.0.1:17080/?op=list

# How to print: 
```javascript
  const printerName = "__PRINTER_NAME_";
  const data = "ANY HTML DATA TO PRINT";

  $.ajax({
            type: 'POST',
            url: 'http://127.0.0.1:17080/?op=print&printerName=' + printerName,
            crossDomain: true,
            data: data,
            dataType: 'html'
        }).done(function (responseData) {
            console.log(responseData);
        }).fail(function (responseData) {
           console.log(responseData);
        });
```