const express = require('express');
const bodyParser = require('body-parser');

const sql = require('mssql');

var config = {
    user: 'admin',
    password: 'tp-iaew-2021',
    server: 'operador-logistico-db.c8f01er7irve.us-east-1.rds.amazonaws.com',
    database:'operador',
    options: {
        encrypt: true,
        trustServerCertificate: true,
    }
};

sql.connect(config, (err) => {
    if (err) {
        console.log(err);
    }
    else{
        console.log("Se conecto");
    }
    

    var request = new sql.Request();

    request.query('select * from envio', (err, recordset) => {
        if (err) {
            
        }
        console.log(recordset);
    });
});

const app = express();

//Para entender JSON
app.use(express.json())

app.use('/repartidores', require('./routes/repartidor.routes'));

//Esperar las peticiones en el puerto 3000
app.listen(3000, () => {
    console.log("Server on port 3000");
});

app.use(bodyParser.json());
