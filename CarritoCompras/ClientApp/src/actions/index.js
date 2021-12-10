import * as api from '../api'
import * as types from '../constants/action-types'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.min.css';
import { filter } from 'rxjs/operators';
import { Subject } from 'rxjs';

const alertSubject = new Subject();
const defaultId = 'default-alert';


/******************************************************************************** Articulo Action ********************************************************************************/
//los metodos de Articulos estan dentro de "articulo-dev-express-list.jsx"


/******************************************************************************** Local Storage ********************************************************************************/

export const refreshUnSafe = ( current ) => ( {
    
    type: types.REFRESH_STORE,
    current
} )

export const refreshStore = ( current ) => dispatch => {
    dispatch( refreshUnSafe( current ) );
}



/******************************************************************************** Marcas Action ********************************************************************************/
export const receiveMarcas = marcas => ( {
    type: types.RECEIVE_MARCAS,
    marcas
} );

export const getAllMarcas = () => dispatch => {
    
    api.getMarcas().then( marcas => {
        dispatch( receiveMarcas( marcas ) );
        return marcas;
    } )
    
}

/******************************************************************************** Modal related Action ********************************************************************************/
// display quickview 
export const showQuickViewModal = articuloId => ( {
    type: types.SHOW_QUICKVIEW,
    articuloId
} )

// close quickview modal
export const closeQuickViewModal = () => ( {
    type: types.CLOSE_QUICKVIEW
} )

// Show Video & Login modal
export const showModal = ( modal ) => ( {
    type: types.SHOW_MODAL,
    modal: modal
} );

// close Video & Login modal
export const closeModal = ( modal ) => ( {
    type: types.CLOSE_MODAL,
    modal: modal
} );

// don't show Newsletter modal
export const removeNewsletterMdoal = ( modal ) => ( {
    type: types.REMOVE_NEWSLETTER
} )

/******************************************************************************** Cart Action ********************************************************************************/
// add item to cart
export const addToCart = ( articulo, qty ) => ( dispatch ) => {
    toast.success( "Articulo agregado al carrito" );
    dispatch( addToCartUnsafe( articulo, qty ) );
}

// add item to cart : typical action
export const addToCartUnsafe = ( articulo, qty ) => ( {
    type: types.ADD_TO_CART,
    articulo,
    qty
} );



// remove item from cart
export const removeFromCart = articuloId => ( dispatch ) => {
    toast.error( "Articulo eliminado del carrito" );

    dispatch( {
        type: types.REMOVE_FROM_CART,
        articuloId
    } )
};

// change item's qty
export const changeQty = ( articuloId, qty ) => ( {
    type: types.CHANGE_QTY,
    articuloId,
    qty
} );

// change shipping method
export const changeShipping = ( shipping ) => ( {
    type: types.CHANGE_SHIPPING,
    shipping
} )



/******************************************************************************** Compare Action ********************************************************************************/
// add to comparelist
export const addToCompare = ( articulo ) => ( dispatch ) => {
    toast.success( "Articulo agregado a la comparación" );
    dispatch( addToCompareUnsafe( articulo ) )
}

export const addToCompareUnsafe = ( articulo ) => ( {
    type: types.ADD_TO_COMPARE,
    articulo
} );

// remove all items from cartlist
export const removeFromCompare = articuloId => ( dispatch ) => {
    toast.success( "Compare item removed" );
    dispatch( removeFromCompareUnsafe( articuloId ) )
};

export const removeFromCompareUnsafe = ( articuloId ) => ( {
    type: types.REMOVE_FROM_COMPARE,
    articuloId
} );

// reset cartlist with intialstate
export const resetCompare = () => ( {
    type: types.RESET_COMPARE
} );


/******************************************************************************** Filter Action ********************************************************************************/

// set order to sort
export const filterSort = ( sortBy ) => ( dispatch ) => {
    dispatch( {
        type: types.SORT_BY,
        sortBy
    } )
}

// set price range to get suitable articulos
export const filterPrice = ( range ) => ( dispatch ) => {
    dispatch( {
        type: types.PRICE_FILTER,
        range
    } )
}

// add/remove category to get suitable articulos
export const toggleCategoryFilter = ( category ) => ( dispatch ) => {
    dispatch( {
        type: types.CATEGORY_FILTER,
        category
    } )
}

// add/remove articulo size to get suitable articulos
export const toggleSizeFilter = ( size ) => ( dispatch ) => {
    dispatch( {
        type: types.SIZE_FILTER,
        size
    } )
}

// add/remove color to get suitable articulos
export const toggleColorFilter = ( color ) => ( dispatch ) => {
    dispatch( {
        type: types.COLOR_FILTER,
        color
    } )
}

// add/remove brand to get suitable articulos
export const toggleBrandFilter = ( brand ) => ( dispatch ) => {
    dispatch( {
        type: types.BRAND_FILTER,
        brand
    } )
}

// add/remove rating to get suitable articulos
export const toggleRatingFilter = ( rating ) => ( dispatch ) => {
    dispatch( {
        type: types.RATING_FILTER,
        rating
    } )
}

// reset filter with intialstate
export const resetFilter = () => ( dispatch ) => {
    dispatch( {
        type: types.RESET_FILTER
    } )
}

/******************************************************************************** Newsletter Modal ********************************************************************************/

// hide newsletter modal in homepage
export const hideNewsletterModal = () => ( {
    type: types.HIDE_NEWSLETTER_MODAL
} )

/******************************************************************************** Alert ********************************************************************************/
export const alertService = {
    onAlert,
    success,
    error,
    info,
    warn,
    alert,
    clear
};

export const AlertType = {
    Success: 'Success',
    Error: 'Error',
    Info: 'Info',
    Warning: 'Warning'
}

// enable subscribing to alerts observable
function onAlert(id = defaultId) {
    return alertSubject.asObservable().pipe(filter(x => x && x.id === id));
}

// convenience methods
function success(message, options) {
    alert({ ...options, type: AlertType.Success, message });
}

function error(message, options) {
    alert({ ...options, type: AlertType.Error, message });
}

function info(message, options) {
    alert({ ...options, type: AlertType.Info, message });
}

function warn(message, options) {
    alert({ ...options, type: AlertType.Warning, message });
}

// core alert method
function alert(alert) {
    alert.id = alert.id || defaultId;
    alert.autoClose = (alert.autoClose === undefined ? true : alert.autoClose);
    alertSubject.next(alert);
}

// clear alerts
function clear(id = defaultId) {
    alertSubject.next({ id });
}




