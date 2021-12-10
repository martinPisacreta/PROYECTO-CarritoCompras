import { BehaviorSubject } from 'rxjs';
import { fetchWrapper, history } from '../components/helpers';

const userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')));
const empresaSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('empresa')));


/******************************************************************************** Articulos ********************************************************************************/
//los metodos de Articulos estan dentro de "articulo-dev-express-list.jsx"


/******************************************************************************** Marcas ********************************************************************************/
export const getMarcas = function () {
    return fetchWrapper.get( `/api/Marcas` )
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
    
}




/******************************************************************************** Usuario ********************************************************************************/
export const usuariosService = {
    login,
    logout,
    register,
    verifyEmail,
    forgotPassword,
    validateResetToken,
    resetPassword,
    //getAll,
    getById,
    //create,
    update,
    delete: _delete,
    user: userSubject.asObservable(),
    get userValue () { return userSubject.value }
};


function login(email, password) { 
    return fetchWrapper.post( `/api/Usuarios/authenticate`, { email, password })
    .then( function ( response ) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('user', JSON.stringify(response));
            
        // publish user to subscribers
        userSubject.next(response);
      

        return response;
    } )
    .catch( function ( error ) {
        throw error
        
    } );
}

//LISTO
function logout() {

    //MAXI QUIERE GUARDAR EL PEDIDO POR MAS QUE SE CERRO SESION
    //localStorage.removeItem('molla-cartlist'); 
    localStorage.removeItem('user');

    // remove user from local storage and publish null to user subject
    userSubject.next(null);
   
}

function register(params) {
    return fetchWrapper.post( `/api/Usuarios/register`, params)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

function verifyEmail(token) {
 
    return fetchWrapper.post( `/api/Usuarios/verify-email`, { token })
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

function forgotPassword(email) {
    return fetchWrapper.post( `/api/Usuarios/forgot-password`, { email })
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

function validateResetToken(token) {
    return fetchWrapper.post( `/api/Usuarios/validate-reset-token`, { token })
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } ); //LISTO
}

function resetPassword({ token, password, confirmacionPassword }) {
    return fetchWrapper.post( `/api/Usuarios/reset-password`, { token, password, confirmacionPassword })
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

// function getAll() {

//     return fetchWrapper.get( `/api/Usuarios`);
// }

function getById(id) {
    
    return fetchWrapper.get( `/api/Usuarios/${id}`)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error;
    } );
}

// function getAddressById(id) {
    
//     return fetchWrapper.get( `/api/Usuarios/getAddressById/${id}`);
// }

// function create(params) {
//     return fetchWrapper.post( `/api/Usuarios`, params);
// }

function update(id, params) {
    
    return fetchWrapper.post( `/api/Usuarios/update/${id}`, params)
        .then(user => {
            // update stored user if the logged in user updated their own record
            if (user.id === userSubject.value.id) {
                // update local storage
                user = { ...userSubject.value, ...user };
                localStorage.setItem('user', JSON.stringify(user));

                // publish updated user to subscribers
                userSubject.next(user);
            }
            return user;
        })
        .catch( function ( error ) {
            throw error 
        } );
}



// prefixed with underscored because delete is a reserved word in javascript
function _delete(id) {
    return fetchWrapper.delete( `/api/Usuarios/${id}`)
        .then(x => {
            // auto logout if the logged in user deleted their own record
            if (id === userSubject.value.id) {
                logout();
            }
            return x;
        })
        .catch( function ( error ) {
            throw error
        } );
}



/******************************************************************************** Empresa ********************************************************************************/
export const empresaService = {
    //getAllEmpresa,
    getByIdEmpresa,
    createEmpresa,
    updateEmpresa,
    deleteEmpresa: _deleteEmpresa,
    empresa: empresaSubject.asObservable(),
    get empresaValue () { return empresaSubject.value }
};


// function getAllEmpresa() {
//     return fetchWrapper.get( `/api/Empresas`);
// }

function getByIdEmpresa(id) {
    return fetchWrapper.get( `/api/Empresas/${id}`)
    .then( function ( response ) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('empresa', JSON.stringify(response));
            
        // publish user to subscribers
        empresaSubject.next(response);
      

        return response;
    } )
    .catch( function ( error ) {
        // handle error
        throw error
        
    } );
}

function createEmpresa(params) {
    return fetchWrapper.post( `/api/Empresas`, params)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

function updateEmpresa(id, params) {
    return fetchWrapper.put( `/api/Empresas/${id}`, params)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

// prefixed with underscored because delete is a reserved word in javascript
function _deleteEmpresa(id) {
    return fetchWrapper.delete( `/api/Empresas/${id}`)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

/******************************************************************************** UsuarioPedidos ********************************************************************************/
export const usuarioPedidosService = {
    //getAllUsuarioPedidos,
    getByIdUsuarioPedidos,
    createUsuarioPedidos,
    updateUsuarioPedidos,
    deleteUsuarioPedidos: _deleteUsuarioPedidos,
};


// function getAllUsuarioPedidos() {
//     return fetchWrapper.get( `/api/UsuarioPedidos`);
// }

function getByIdUsuarioPedidos(id_usuario) {
    return fetchWrapper.get( `/api/UsuarioPedidos/${id_usuario}`)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

function createUsuarioPedidos(params) {
    return fetchWrapper.post( `/api/UsuarioPedidos`, params)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

function updateUsuarioPedidos(id, params) {
    return fetchWrapper.put( `/api/UsuarioPedidos/${id}`, params)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

// prefixed with underscored because delete is a reserved word in javascript
function _deleteUsuarioPedidos(id) {
    return fetchWrapper.delete( `/api/UsuarioPedidos/${id}`)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}


/******************************************************************************** UsuarioPedidoDetalles ********************************************************************************/
export const usuarioPedidoDetallesService = {
    //getAllUsuarioPedidoDetalles,
    getUsuarioPedidoDetallesByIdUsuarioPedido, //traigo el pedido detalle en base al id_usuario_pedido
    createUsuarioPedidoDetalles,
    updateUsuarioPedidoDetalles,
    deleteUsuarioPedidoDetalles: _deleteUsuarioPedidoDetalles,
};


// function getAllUsuarioPedidoDetalles() {
//     return fetchWrapper.get( `/api/UsuarioPedidoDetalles`);
// }

function getUsuarioPedidoDetallesByIdUsuarioPedido(id_usuario_pedido) {
    return fetchWrapper.get( `/api/UsuarioPedidoDetalles/${id_usuario_pedido}`)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

function createUsuarioPedidoDetalles(params) {
    return fetchWrapper.post( `/api/UsuarioPedidoDetalles`, params)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

function updateUsuarioPedidoDetalles(id, params) {
    return fetchWrapper.put( `/api/UsuarioPedidoDetalles/${id}`, params)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

// prefixed with underscored because delete is a reserved word in javascript
function _deleteUsuarioPedidoDetalles(id) {
    return fetchWrapper.delete( `/api/UsuarioPedidoDetalles/${id}`)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

/******************************************************************************** ArticulosUploadImage ********************************************************************************/

export const articulosUploadImageService = {
    uploadFilesInServer,
};



function uploadFilesInServer(params) {
    return fetchWrapper.postSinCovertirBodyEnJson( `/api/ArticulosUploadImage`, params)
    .then( function ( response ) {
        return response;
    } )
    .catch( function ( error ) {
        throw error
    } );
}

