import React, { useEffect, useState } from 'react';
import { Link,NavLink } from 'react-router-dom';
import { usuariosService } from '../../../api';

export default function MainMenu( props ) {
    const [ path, setPath ] = useState( "" );
    const [user, setUser] = useState({});


    useEffect( () => {
        setPath( window.location.href );
    } )

    //voy a buscar los datos del usuario logueado y despues interrumpo el flujo "subscription.unsubscribe();""
    useEffect( () => { 
        const subscription = usuariosService.user.subscribe(x => setUser(x));
        return subscription.unsubscribe();
        
    }, [] )


    function showAllDemos( e ) {
        let demoItems = document.querySelectorAll( '.demo-item.hidden' );

        for ( let i = 0; i < demoItems.length; i++ ) {
            demoItems[ i ].classList.toggle( 'show' );
        }

        document.querySelector( '.view-all-demos' ).classList.toggle( 'disabled-hidden' );
        e.preventDefault();
    }

    return (

        
        <nav className="main-nav">
        <ul className="menu sf-arrows">
            <li className={ `megamenu-container` } id="menu-home">
                <Link  to={ `${process.env.PUBLIC_URL}` } className="sf-with">Inicio</Link>
            </li>

            {
                user && 
                <li className={ `megamenu-container` } id="menu-home">
                    <Link to={ `${process.env.PUBLIC_URL}/usuario/dashboard` } className="sf-with">Mi Cuenta</Link>
                </li>
            } 


            <li className={ `megamenu-container` } id="menu-home">
                <Link  to={ `${process.env.PUBLIC_URL}/catalogo/list` } className="sf-with">Catalogo</Link>
            </li>

            {
                user && user.rol === 'Admin' &&
                <li className={ `megamenu-container` } id="menu-home">
                    <Link to={ `${process.env.PUBLIC_URL}/usuario/admin_imagenes_articulos` } className="sf-with">Imagenes Articulos</Link>
                </li>
            } 

            {/* <li className={ `megamenu-container` } id="menu-home">
                <Link to={ `${process.env.PUBLIC_URL}/pages/about` } className="sf-with"> ¿Quienes somos?</Link>
            </li> */}

        </ul>
    </nav>
    );
}