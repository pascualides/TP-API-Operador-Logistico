
const configdb = require('./config');
const sql = require('mssql');

const envio_ctrl = {};

envio_ctrl.getEnvios = async(req, res) => {
    sql.connect(configdb, (err) => {
        if (err) {
            console.log(err);
        }
        var request = new sql.Request();
    
        request.query("select * from envio e join contacto c on e.dniContacto = c.documento join producto p on e.nroProducto = p.id", (err, recordset) => {
            if (err) {
                
            }
            res.status(200).json(recordset.recordset);               
        });

    });
};


envio_ctrl.getEnvio = async(req, res) => {
    let id = req.params.id
    sql.connect(configdb, (err) => {
        if (err) {
            console.log(err);
        }
        var request = new sql.Request();
    
        request.query("select * from envio e join contacto c on e.dniContacto = c.documento join producto p on e.nroProducto = p.id where e.id ="+id, (err, recordset) => {
            if (err) {
                
            }
            res.status(200).json(recordset.recordset);               
        });

    });
};


envio_ctrl.asignarRepartidor = async(req, res) => {
    // select documento from repartidor where documento not inselect repartidor from envio 
    // poner el estado en transito a envio
};

envio_ctrl.setEntrega = async(req, res) => {
    //Cambiar estado envio a entregado y setear fecha de entrega
};

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

        //Insertat producto
        var request = new sql.Request();
    
        request.query("insert into producto(peso, tamaño, delicado) values ("+envio.producto.peso+", '"+envio.producto.tamaño+"', '"+envio.producto.delicado+"') SELECT SCOPE_IDENTITY() as id", (err, recordset) => {
            if (err) {
                
            }
            if(recordset.rowsAffected[0]){
                nroProducto = recordset.recordset[0].id;



            //INSERTAR ENVIO

            // insert into envio (provinciaDestino, codPostalDestino, calleDestino, nroDestino, provinciaOrigen, codPostalOrigen, calleOrigen, nroOrigen, dniContacto, nroProducto, estado, fechaRecepcion)
            // values ('cordoba', 5000, 'sarmiento', 321, 'Salta', 3000, 'san martin', 546, 23, 3, 'creado', getdate()) 

                let request2 = new sql.Request();

                let queryEnvio = " insert into envio (provinciaDestino, codPostalDestino, localidadDestino, calleDestino, nroDestino, provinciaOrigen, codPostalOrigen, localidadOrigen, calleOrigen, nroOrigen, dniContacto, nroProducto, estado, fechaRecepcion) "

                queryEnvio += "values ('"+envio.destino.provincia+"', "+envio.destino.codPostal+", '"+envio.destino.localidad+"', '"+envio.destino.calle+"', "+envio.destino.nro+", "
        
                queryEnvio += "'"+envio.origen.provincia+"', "+envio.origen.codPostal+", '"+envio.origen.localidad+"', '"+envio.origen.calle+"', "+envio.origen.nro
        
                queryEnvio += ", "+envio.contacto.documento+", "+nroProducto+", 'creado', getdate())"
           
                request2.query(queryEnvio, (err, recordset) => {
                    if (err) {
                        
                    }
                        res.status(200).json({status: "Envio creado con exito"})
                    
                });



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
        });

    });
};






module.exports = envio_ctrl;