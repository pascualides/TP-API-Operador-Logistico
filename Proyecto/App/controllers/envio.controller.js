
const configdb = require('./config');
const sql = require('mssql');

const envio_ctrl = {};

envio_ctrl.crearEnvio = async(req, res) => {

    let nroProducto = 0;

    let envio = {
        id:"",
        origen: {
            calle: req.body.origen.calle,
            localidad: req.body.origen.localidad,
            provincia: req.body.origen.provincia,
            nro: req.body.origen.nro,
            codPostal: req.body.origen.codPostal
        },

        destino: {
            calle: req.body.destino.calle,
            localidad: req.body.destino.localidad,
            provincia: req.body.destino.provincia,
            nro: req.body.destino.nro,
            codPostal: req.body.destino.codPostal
        },

        contacto: {
            documento: req.body.contacto.documento,
            nombre: req.body.contacto.nombre,
            apellido: req.body.contacto.apellido,
            email: req.body.contacto.email
        },

        producto: {
            peso: req.body.producto.peso,
            tamaño: req.body.producto.tamaño,
            delicado: req.body.producto.delicado
        },
        estado: "creado",
        fecha_recepcion: Date.now()

    }

    sql.connect(configdb, (err) => {
        if (err) {
            console.log(err);
        }
        var request = new sql.Request();
    
        request.query("insert into producto(peso, tamaño, delicado) values ("+envio.producto.peso+", '"+envio.producto.tamaño+"', '"+envio.producto.delicado+"') SELECT SCOPE_IDENTITY() as id", (err, recordset) => {
            if (err) {
                
            }
            if(recordset.rowsAffected[0]){
                nroProducto = recordset.recordset[0].id;
            }
        });

        request = new sql.Request();


//         If Not Exists(select * from  contacto where documento = 1234)
// Begin
// insert into contacto (documento, nombre, apellido, email) VALUES (1234, 'UN', 'Ape', 'mail')
// End

        let query = "If Not Exists(select * from contacto where documento = "+envio.contacto.documento+")";
        query += " Begin ";
        query += " insert into contacto (documento, nombre, apellido, email) VALUES ("+envio.contacto.documento+", '"+envio.contacto.nombre+"', '"+envio.contacto.apellido+"', '"+envio.contacto.email+"') ";
        query += " End "


        request.query(query, (err, recordset) => {
            if (err) {
                
            }
            if(recordset.rowsAffected[0]){
                nroProducto = recordset.recordset[0].id;
            }
        });

        //FALTA INSERTAR ENVIO

    });
};


module.exports = envio_ctrl;