# TP-API-Operador-Logistico


## URL

https://01ic9vqqt5.execute-api.us-east-1.amazonaws.com/Prod

## Carga de ordenes de envio

**URL**
https://01ic9vqqt5.execute-api.us-east-1.amazonaws.com/Prod/ordendes_envio

**Metodo**
Post

**Json de ejemplo**
```go
```json
{

"provinciaDestino":"Tucuman",

"localidadDestino":"Tafi Viejo",

"codPostalDestino": 6000,

"calleDestino": "Peru",

"nroDestino": 260,

"provinciaOrigen":"Cordoba",

"localidadOrigen":"cordoba",

"codPostalOrigen": 5014,

"calleOrigen": "Capdevila",

"nroOrigen": 1680,

"pesoProd": 3000,

"tamañoProd": "mediano",

"delicado":"true",

"contacto":{

	"documento": 33268842,

	"nombre":"Camila",

	"apellido": "Oliva",

	"email": "oliva@gmail.com"

	}

}
```
```
