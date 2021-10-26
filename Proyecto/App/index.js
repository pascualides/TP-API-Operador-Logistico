const express = require('express');
const bodyParser = require('body-parser');

const sql = require('mssql');


const app = express();

//Para entender JSON
app.use(express.json())

app.use('/repartidores', require('./routes/repartidor.routes'));

//Esperar las peticiones en el puerto 3000
app.listen(3000, () => {
    console.log("Server on port 3000");
});

app.use(bodyParser.json());
