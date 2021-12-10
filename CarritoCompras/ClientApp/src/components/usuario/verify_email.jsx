import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import queryString from 'query-string';
import { Helmet } from 'react-helmet';
import PageHeader from '../common/page-header';
import Breadcrumb from '../common/breadcrumb';
import { usuariosService} from '../../api';
import {alertService} from '../../actions'
import {Alert} from '../alert'
import './form-control.css'


function VerifyEmail({ history,location }) {
    const EmailStatus = {
        Verifying: 'Verifying',
        Failed: 'Failed'
    }

    const [emailStatus, setEmailStatus] = useState(EmailStatus.Verifying);

    useEffect(() => {
       
        const { token } = queryString.parse(location.search);

        // remove token from url to prevent http referer leakage
        history.replace(location.pathname);

        usuariosService.verifyEmail(token)
            .then(() => {
                alertService.success('Verificación exitosa, ahora puede iniciar sesión', { keepAfterRouteChange: true });
                const timeout = setTimeout(() => {
                    history.push('/');
                  }, 3000);
            })
            .catch(() => {
                setEmailStatus(EmailStatus.Failed);
            });
    }, []);

    function getBody() {
        switch (emailStatus) {
            case EmailStatus.Verifying:
                return <div>Verificando...</div>;
            case EmailStatus.Failed:
                return <div>La verificación falló, también puede verificar su cuenta usando el <Link to="forgot-password">Olvide mi contraseña</Link></div>;
        }
    }

    return (

        <>
            <Helmet>
                <title>Encendido Alsina - Verificar email</title>
            </Helmet>
            
            <h1 className="d-none">Encendido Alsina - Verificar email</h1>
            
            <div className="main">
                <PageHeader  subTitle="Verificar email" />
                <Breadcrumb title="Verificar email" parent1={ [ "Iniciar Sesión", "usuario/login" ] } />
            
                <div className="page-content">
                        <div className="container">
                            <Alert/>
                            <div>
                                <h3 className="card-header">Verificar email</h3>
                                <div className="card-body">{getBody()}</div>
                            </div>
                        </div>
                    </div>
                </div>
        </>
    )
}

export default React.memo( VerifyEmail );
