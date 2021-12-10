import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Helmet } from 'react-helmet';

// import Custom Components
import PageHeader from '../../common/page-header';
import Breadcrumb from '../../common/breadcrumb';
import IconBox from '../../features/icon-box';

import { countTo } from '../../../utils';


function AboutTwo() {
    useEffect( () => {
        countTo();
    } )


    const backgroundStyle = {
        backgroundImage: `linear-gradient(to bottom, rgba(0, 0, 0, 0.9), rgba(0, 0, 0, 0.9)), url(${process.env.PUBLIC_URL}/assets/images/backgrounds/bg-4.jpg)`,
        backgroundSize: "cover",
    }


    return (
        <div className="main">
            <Helmet>
                <title>Encendido Alsina - ¿Quienes somos?</title>
            </Helmet>

            <h1 className="d-none">Encendido Alsina - ¿Quienes somos?</h1>

            <PageHeader  subTitle="Encendido Alsina" />
            <Breadcrumb parent1={ [ "¿Quienes somos?", "pages/about" ] } />


            <div className="page-content pb-3">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-10 offset-lg-1">
                            <div className="about-text text-center mt-3">
                                <h2 className="title text-center mb-2">¿Quienes somos?</h2>
                                <p>Descripcion de quien es ENCENDIDO ALSINA. </p>
                                <br></br>
                                {/* <img src={ `${process.env.PUBLIC_URL}/assets/images/about/about-2/signature.png` } alt="signature" className="mx-auto mb-5" /> */}
                                <img src={ `${process.env.PUBLIC_URL}/assets/images/about/about-2/img-1.jpg` } alt="temp" className="mx-auto mb-6" />
                            </div>
                        </div>
                    </div>

                    <div className="row justify-content-center">
                        <div className="col-lg-4 col-sm-6">
                            <IconBox boxStyle="icon-box-sm text-center" iconClass="icon-puzzle-piece" title="Calidad de diseño" text="" />
                        </div>
                        <div className="col-lg-4 col-sm-6">
                            <IconBox boxStyle="icon-box-sm text-center" iconClass="icon-life-ring" title="Soporte profesional" text="" />
                        </div>
                        <div className="col-lg-4 col-sm-6">
                            <IconBox boxStyle="icon-box-sm text-center" iconClass="icon-heart-o" title="Hecho con esfuerzo" text="" />
                        </div>
                    </div>
                </div>

                <div className="mb-2"></div>

                <div className="bg-image pt-7 pb-5 pt-md-12 pb-md-9" style={ backgroundStyle }>
                    <div className="container">
                        <div className="row">
                            <div className="col-6 col-md-6">
                                <div className="count-container text-center">
                                    <div className="count-wrapper text-white">
                                        <span className="count" data-from="0" data-to="40" data-speed="3000" data-refresh-interval="50">0</span>k+
                                    </div>
                                    <h3 className="count-title text-white">Clientes Felices</h3>
                                </div>
                            </div>

                            <div className="col-6 col-md-6">
                                <div className="count-container text-center">
                                    <div className="count-wrapper text-white">
                                        <span className="count" data-from="0" data-to="20" data-speed="3000" data-refresh-interval="50">0</span>+
                                    </div>
                                    <h3 className="count-title text-white">Años en el negocio</h3>
                                </div>
                            </div>

                            {/* <div className="col-6 col-md-3">
                                <div className="count-container text-center">
                                    <div className="count-wrapper text-white">
                                        <span className="count" data-from="0" data-to="95" data-speed="3000" data-refresh-interval="50">0</span>%
                                    </div>
                                    <h3 className="count-title text-white">Return Clients</h3>
                                </div>
                            </div>

                            <div className="col-6 col-md-3">
                                <div className="count-container text-center">
                                    <div className="count-wrapper text-white">
                                        <span className="count" data-from="0" data-to="15" data-speed="3000" data-refresh-interval="50">0</span>
                                    </div>
                                    <h3 className="count-title text-white">Awards Won</h3>
                                </div>
                            </div> */}
                        </div>
                    </div>
                </div>

                {/* <div className="bg-light-2 pt-6 pb-7 mb-6">
                    <div className="container">
                        <h2 className="title text-center mb-4">Meet Our Team</h2>

                       

                        <div className="text-center mt-3">
                            <Link to={ `${process.env.PUBLIC_URL}/blog/classic` } className="btn btn-sm btn-minwidth-lg btn-outline-primary-2">
                                <span>LETS START WORK</span>
                                <i className="icon-long-arrow-right"></i>
                            </Link>
                        </div>
                    </div>
                </div> */}

                {/* <div className="container">
                    <div className="row">
                        <div className="col-lg-10 offset-lg-1">
                            <div className="brands-text text-center mx-auto mb-6">
                                <h2 className="title">The world's premium design brands in one destination.</h2>
                                <p>Phasellus hendrerit. Pellentesque aliquet nibh nec urna. In nisi neque, aliquet vel, dapibus id, mattis vel, nis</p>
                            </div>

                           
                        </div>
                    </div>
                </div> */}
            </div>
        </div>
    )
}

export default React.memo( AboutTwo );