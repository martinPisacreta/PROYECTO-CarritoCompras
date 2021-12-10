import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { usuariosService } from '../../../api';
import { mobileMenu } from '../../../utils';

function MobileMainNav( props ) {

    const [user, setUser] = useState({});

    React.useEffect( () => {
        mobileMenu();
    } )

    //voy a buscar los datos del usuario logueado y despues interrumpo el flujo "subscription.unsubscribe();""
    useEffect( () => { 
        const subscription = usuariosService.user.subscribe(x => setUser(x));
        return subscription.unsubscribe();
        
    }, [] )


    return (
        <nav className="mobile-nav">
            <ul className="mobile-menu">
                <li>
                    <Link to={ `${process.env.PUBLIC_URL}` }>
                        Inicio
                    </Link>
                </li>

                {
                    user && 
                    <li>
                        <Link to={ `${process.env.PUBLIC_URL}/usuario/dashboard` }>
                            Mi Cuenta
                        </Link>
                    </li>
                } 

                <li>
                    <Link to={ `${process.env.PUBLIC_URL}/catalogo/list` }>
                        Catalogo
                    </Link>
                </li>

                {
                    user && user.rol === 'Admin' &&
                    <li>
                        <Link to={ `${process.env.PUBLIC_URL}/usuario/admin_imagenes_articulos` }>
                            Imagenes Articulos
                        </Link>
                    </li>
                } 

                {/* <li>
                    <Link to={ `${process.env.PUBLIC_URL}/pages/about` } className="sf-with-ul">
                        ¿Quienes somos?
                    </Link>
                </li> */}
            </ul>
        </nav>
    );
}

export default MobileMainNav;