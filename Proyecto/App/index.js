const express = require('express');
const bodyParser = require('body-parser');

const sql = require('mssql');

var config = {
    user: 'admin',
    password: 'tp-iaew-2021',
    server: '',
    database:'operador'
};

sql.connect(config, (err) => {
    if (err) {
        console.log(err);
    }

    var request = new sql.Request();

    request.query('select* from empleados', (err, recordset) => {
        if (err) {
            
        }
        //res.send(recordset)
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
