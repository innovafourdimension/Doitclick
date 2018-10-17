TRUNCATE `Etapas`;
INSERT INTO `Etapas` (`Id`, `ProcesoId`, `TipoEtapa`, `Nombre`, `NombreInterno`, `TipoUsuarioAsignado`, `ValorUsuarioAsignado`, `TipoDuracion`, `ValorDuracion`, `TipoDuracionRetardo`, `ValorDuracionRetardo`, `Secuencia`) VALUES

(1,	1,	'Inicio',	'Inicio Proceso',	'InicioProceso',	'Boot',	'wfboot',	'Ninguna',	NULL,	'Ninguna',	NULL,	100),
(2,	1,	'Actividad',	'Ingreso Datos Paciente',	'IngresoDatosPaciente',	'Usuario',	'17042783-1',	'Ninguna',	NULL,	'Ninguna',	NULL,	200),
(5,	1,	'Actividad',	'Asignar Trabajo',	'AsignarTrabajo',	'Usuario',	'17042783-1',	'Ninguna',	NULL,	'Ninguna',	NULL,	400),
(6,	1,	'Actividad',	'Asignar Cotización',	'AsignarCotizacion',	'Usuario',	'17042783-1',	'Ninguna',	NULL,	'Ninguna',	NULL,	400);