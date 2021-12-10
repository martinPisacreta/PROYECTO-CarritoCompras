import { findIndex } from '../utils';

/**
 * get total Price of articulos in cart.
 * @param {Array} cartItems 
 * @return {Float} totalPrice
 */
export const getCartTotal = cartItems => {
    let total = 0;

    for ( let i = 0; i < cartItems.length; i++ ) {
        total += parseInt( cartItems[ i ].qty, 10 ) * ( cartItems[ i ].precioLista_por_coeficiente_por_medioIva );
    }
    return total;
}

/**
 * get number of articulos in cart
 * @param {Array} cartItems 
 * @return {Integer} numbers of cart items in cartlist
 */
export const getCartCount = cartItems => {
    let total = 0;

    for ( let i = 0; i < cartItems.length; i++ ) {
        total += parseInt( cartItems[ i ].qty, 10 );
    }

    return total;
}

