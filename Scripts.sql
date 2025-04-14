INSERT INTO usuarios (UserName, Password, Email, FechaNacimiento, Telefono, State, IsNoVidente, IsAdmin, Documento)
VALUES 
(
    'admin_user',
    '123456',  -- Hashed password de "1234"
    'admin@example.com',
    '1990-05-20 00:00:00.000000',
    '1234567890',
    1,  -- State = Activo
    0,  -- IsNoVidente = false
    1,  -- IsAdmin = true
    123456789
);


INSERT INTO categorias (Descripcion, CreatedDate, State, UsuarioId)
VALUES
    ('Museos', NOW(6), 1, 1),
    ('Iglesias', NOW(6), 1, 1),
    ('Entretenimiento', NOW(6), 1, 1),
    ('Bares y Restaurantes', NOW(6), 1, 1),
    ('Naturaleza', NOW(6), 1, 1),
    ('Imperdibles', NOW(6), 1, 1),
    ('Fiestas y Tradiciones', NOW(6), 1, 1);

   
INSERT INTO puntosturisticos (Nombre, Descripcion, CategoriaId, Latitud, Longitud, PathImagen, State, UsuarioId, CreatedDate)
VALUES
    ('Museo Casa Histórica de la Independencia', 'Histórico museo donde se firmó la independencia de Argentina.', 1, '-26.83312', '-65.20380', '', 1, 1, NOW(6)),
    ('Museo Miguel Lillo de Ciencias Naturales', 'Museo de historia natural con una importante colección de especies.', 1, '-26.83143', '-65.22104', '', 1, 1, NOW(6)),
    ('Casa de Gobierno', 'Sede del Poder Ejecutivo de Tucumán, de gran valor arquitectónico.', 1, '-26.82999', '-65.20478', '', 1, 1, NOW(6)),
    ('Plaza Independencia', 'Plaza principal de la ciudad, con la estatua de la libertad.', 1, '-26.83044', '-65.20381', '', 1, 1, NOW(6)),
    ('Museo Histórico Nacional Nicolás Avellaneda', 'Museo dedicado a la historia y cultura de Tucumán.', 1, '-26.83199', '-65.20266', '', 1, 1, NOW(6));


INSERT INTO puntosturisticos (Nombre, Descripcion, CategoriaId, Latitud, Longitud, PathImagen, State, UsuarioId, CreatedDate)
VALUES
    ('Iglesia San Francisco', 'Iglesia histórica con una hermosa arquitectura.', 2, '-26.82772', '-65.19877', '', 1, 1, NOW(6)),
    ('Catedral de San Miguel de Tucumán', 'Catedral principal de la ciudad, de gran valor religioso e histórico.', 2, '-26.83121', '-65.20288', '', 1, 1, NOW(6)),
    ('Iglesia Nuestra Señora de la Merced', 'Templo católico con un diseño colonial.', 2, '-26.831321', '-65.201359', '', 1, 1, NOW(6)),
    ('Iglesia María Auxiliadora', 'Iglesia con una comunidad muy activa.', 2, '-26.83528', '-65.20873', '', 1, 1, NOW(6)),
    ('Parroquia Sagrado Corazón de Jesús', 'Lugar de culto con hermosos vitrales.', 2, '-26.83784', '-65.20918', '', 1, 1, NOW(6));


INSERT INTO puntosturisticos (Nombre, Descripcion, CategoriaId, Latitud, Longitud, PathImagen, State, UsuarioId, CreatedDate)
VALUES
    ('Teatro San Martín', 'El teatro más importante de Tucumán.', 3, '-26.81783', '-65.20296', '', 1, 1, NOW(6)),
    ('Teatro Alberdi', 'Histórico teatro universitario.', 3, '-26.8309', '-65.2109', '', 1, 1, NOW(6)),
    ('Teatro Mercedes Sosa', 'Teatro con espectáculos de primer nivel.', 3, '-26.82955', '-65.20425', '', 1, 1, NOW(6)),
    ('Cine Atlas Vía 24', 'Cine moderno con múltiples salas.', 3, '-26.82957', '-65.20867', '', 1, 1, NOW(6)),
    ('Shopping El Portal', 'Centro comercial con múltiples tiendas y entretenimiento.', 3, '-26.821453', '-65.26659', '', 1, 1, NOW(6));
   

INSERT INTO puntosturisticos (Nombre, Descripcion, CategoriaId, Latitud, Longitud, PathImagen, State, UsuarioId, CreatedDate)
VALUES
    ('Espacio Juntarnos (Bar Inclusivo)', 'Bar inclusivo con actividades culturales.', 4, '-26.8280', '-65.2040', '', 1, 1, NOW(6)),
    ('La Pizzada', 'Pizzería famosa en la ciudad.', 4, '-26.83049', '-65.204419', '', 1, 1, NOW(6)),
    ('Filipo', 'Restaurante con especialidad en pastas.', 4, '-26.82806', '-65.20415', '', 1, 1, NOW(6)),
    ('Patagonia', 'Cervecería con gran variedad de estilos.', 4, '-26.79649', '-65.25651', '', 1, 1, NOW(6)),
    ('Beans 25', 'Cafetería con un ambiente relajado.', 4, '-26.8284', '-65.20408', '', 1, 1, NOW(6));
   
   
INSERT INTO puntosturisticos (Nombre, Descripcion, CategoriaId, Latitud, Longitud, PathImagen, State, UsuarioId, CreatedDate)
VALUES
    ('Parque Avellaneda', 'Espacio verde ideal para caminar y hacer ejercicio.', 5, '-26.82592', '-65.22390', '', 1, 1, NOW(6)),
    ('Parque 9 de Julio', 'El parque más grande de la ciudad.', 5, '-26.82889', '-65.19095', '', 1, 1, NOW(6)),
    ('Plaza Urquiza', 'Plaza tradicional con árboles y monumentos.', 5, '-26.81908', '-65.20243', '', 1, 1, NOW(6)),
    ('Dique La Angostura', 'Hermoso dique con paisajes montañosos.', 5, '-26.92263', '-65.69707', '', 1, 1, NOW(6)),
    ('Reserva Experimental Horco Molle', 'Reserva natural con variedad de flora y fauna.', 5, '-26.79277', '-65.31548', '', 1, 1, NOW(6));

   
 INSERT INTO puntosturisticos (Nombre, Descripcion, CategoriaId, Latitud, Longitud, PathImagen, State, UsuarioId, CreatedDate)
VALUES
    ('Cerro San Javier', 'Cerro con vistas espectaculares.', 6, '-26.79570', '-65.35957', '', 1, 1, NOW(6)),
    ('Quebrada de Lules', 'Paisaje natural ideal para senderismo.', 6, '-26.88271', '-65.41289', '', 1, 1, NOW(6)),
    ('Tafí del Valle', 'Hermoso valle turístico en los Valles Calchaquíes.', 6, '-26.85170', '-65.70836', '', 1, 1, NOW(6)),
    ('Amaicha', 'Pueblo con tradiciones indígenas y paisajes increíbles.', 6, '-26.59305', '-65.92004', '', 1, 1, NOW(6)),
    ('Ruinas de Quilmes', 'Ruinas arqueológicas de los pueblos originarios Quilmes.', 6, '-26.46733', '-66.03263', '', 1, 1, NOW(6));

  
INSERT INTO eventos (Nombre, Descripcion, FechaInicio, FechaFin, CategoriaId, UsuarioId, State, PathImagen, Latitud, Longitud, CreatedDate)
VALUES
    ('Fiesta Nacional del Queso', 'Evento gastronómico en Tafí del Valle con degustaciones, ferias y espectáculos.', NOW(6), '2024-04-30 23:59:59.000000', 7, 1, 1, '', '-26.85170', '-65.70836', NOW(6)),
    ('Carnaval de San Pedro de Colalao', 'Desfile de comparsas y festividades tradicionales del carnaval tucumano.', NOW(6), '2024-04-30 23:59:59.000000', 7, 1, 1, '', '-26.2583', '-65.4961', NOW(6)),
    ('Festival de la Empanada', 'Evento culinario con degustaciones, concursos y espectáculos en Famaillá.', NOW(6), '2024-04-30 23:59:59.000000', 7, 1, 1, '', '-27.0500', '-65.4000', NOW(6)),
    ('Fiesta de la Pachamama', 'Ceremonia en honor a la Madre Tierra, con rituales ancestrales en Amaicha del Valle.', NOW(6), '2024-04-30 23:59:59.000000', 7, 1, 1, '', '-26.59305', '-65.92004', NOW(6)),
    ('Rock and Vida Tucumán', 'Festival de música solidario con bandas locales y nacionales.', NOW(6), '2024-04-30 23:59:59.000000', 7, 1, 1, '', '-26.83644', '-65.20622', NOW(6)),
    ('Fiesta Nacional del Limón', 'Celebración de la producción citrícola en la provincia, con desfiles y actividades culturales.', NOW(6), '2024-04-30 23:59:59.000000', 7, 1, 1, '', '-26.8169', '-65.1556', NOW(6)),
    ('Feria de Artesanos en Plaza Urquiza', 'Feria con exhibición y venta de productos artesanales tucumanos.', NOW(6), '2024-04-30 23:59:59.000000', 7, 1, 1, '', '-26.81908', '-65.20243', NOW(6)),
    ('Encuentro Nacional de Danzas Folklóricas', 'Competencia de danzas tradicionales en San Miguel de Tucumán.', NOW(6), '2024-04-30 23:59:59.000000', 7, 1, 1, '', '-26.8322', '-65.2048', NOW(6)),
    ('Noche de los Museos', 'Jornada donde museos y centros culturales abren sus puertas con actividades especiales.', NOW(6), '2024-04-30 23:59:59.000000', 7, 1, 1, '', '-26.83312', '-65.20380', NOW(6)),
    ('Expo Lules Productivo', 'Feria de producción agrícola y emprendimientos locales en Lules.', NOW(6), '2024-04-30 23:59:59.000000', 7, 1, 1, '', '-26.9263', '-65.3300', NOW(6));

   
