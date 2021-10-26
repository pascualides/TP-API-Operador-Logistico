const express = require('express');

const router = express.Router();


const envio_ctrl = require('../controllers/envio.controller')

// router.get('/', repartidor_ctrl.getRepartidores);

// router.get('/:id', repartidor_ctrl.getRepartidor);

// router.delete('/:id', repartidor_ctrl.deleteRepartidor);

// router.post('/', repartidor_ctrl.createRepartidor);

// router.post('/:id', repartidor_ctrl.updateRepartidor);

router.post('/', envio_ctrl.crearEnvio)

module.exports = router;
