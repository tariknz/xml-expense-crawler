# Overview

This application will take in arbitrary text and will extract expense xml from it

# Demo

https://xml-expense-crawler.azurewebsites.net/swagger

```
"Hello world,

Here is an exmaple expense:
<expense>
<cost_centre>DEV002</cost_centre>
<total>890.55</total><payment_method>personal card</payment_method>
</expense>

Thanks!"
```

> Keep the quotation marks when testing in Swagger

Will return:

```
{
  "costCentre": "DEV002",
  "total": 890.55,
  "paymentMethod": "personal card"
}
```
