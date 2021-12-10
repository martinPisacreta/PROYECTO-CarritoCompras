import React, { useState, useEffect } from 'react';
import { Link,useLocation,Redirect } from 'react-router-dom';
import { LazyLoadImage } from 'react-lazy-load-image-component';
import { Helmet } from 'react-helmet';
import store from '../../store';

// import Custom Components
import OwlCarousel from '../features/owl-carousel';
import {getMarcas} from '../../api';
import IntroSlider from './intro_slider';
import { introSlider } from '../settings';
import style from './style.scss';


import { usuariosService } from '../../api';
import {alertService} from '../../actions'

export default function HomePage( props ) {
    const [marcas,setMarcas] = useState([]) 
    const { pathname } = useLocation();  

    const [user, setUser] = useState({}); 


    const data =  
    [
        {
            "image": "assets/images/home/sliders/slide-8.jpg",
            "subtitle": "Trade-In Offer",
            "title": "MacBook Air <br />Latest Model<span><sup class='font-weight-light'>from</sup><span class='text-primary'>$999<sup>,99</sup></span></span>"
        },
        {
            "image": "assets/images/home/sliders/slide-2.jpg",
            "subtitle": "Trevel & Outdoor",
            "title": "Original Outdoor <br />Beanbag<span><sup class='font-weight-light line-through'>$89,99</sup><span class='text-primary'>$29<sup>,99</sup></span></span>"
        },
        {
            "image": "assets/images/home/sliders/slide-7.jpg",
            "subtitle": "Fashion Promotions",
            "title": "Tan Suede <br />Biker Jacket<span><sup class='font-weight-light line-through'>$240,00</sup><span class='text-primary'>$180<sup>,99</sup></span></span>"
        },
        // {
        //     "image": "assets/images/home/sliders/slide-8.jpg",
        //     "subtitle": "Fashion Promotions",
        //     "title": "Tan Suede <br />Biker Jacket<span><sup class='font-weight-light line-through'>$240,00</sup><span class='text-primary'>$180<sup>,99</sup></span></span>"
        // },
        {
            "image": "assets/images/home/sliders/slide-9.jpg",
            "subtitle": "Fashion Promotions",
            "title": "Tan Suede <br />Biker Jacket<span><sup class='font-weight-light line-through'>$240,00</sup><span class='text-primary'>$180<sup>,99</sup></span></span>"
        },
        {
            "image": "assets/images/home/sliders/slide-10.jpg",
            "subtitle": "Fashion Promotions",
            "title": "Tan Suede <br />Biker Jacket<span><sup class='font-weight-light line-through'>$240,00</sup><span class='text-primary'>$180<sup>,99</sup></span></span>"
        },
        // {
        //     "image": "assets/images/home/sliders/slide-7.jpg",
        //     "subtitle": "Fashion Promotions",
        //     "title": "Tan Suede <br />Biker Jacket<span><sup class='font-weight-light line-through'>$240,00</sup><span class='text-primary'>$180<sup>,99</sup></span></span>"
        // },
        {
            "image": "assets/images/home/sliders/slide-11.jpg",
            "subtitle": "Fashion Promotions",
            "title": "Tan Suede <br />Biker Jacket<span><sup class='font-weight-light line-through'>$240,00</sup><span class='text-primary'>$180<sup>,99</sup></span></span>"
        }
    ]



    
    
    useEffect( () => {
        document.getElementById( "menu-home" ).classList.add( "active" );
  
        
        style.use();

        return ( () => {
            document.getElementById( "menu-home" ).classList.remove( "active" );
            style.unuse();
           
        } )
    }, [] )
    
    //voy a buscar los datos del usuario logueado y despues interrumpo el flujo "subscription.unsubscribe();""
    useEffect( () => { 
        const subscription = usuariosService.user.subscribe(x => setUser(x));
        return subscription.unsubscribe();
       
    }, [] )
    

    useEffect(() => {
        getMarcas()
            .then(x => setMarcas(x))
            .catch(error => {
                alertService.error(error);
            });
      }, []);


    return (
        <>
          

            <Helmet>
                <title>Encendido Alsina</title>
            </Helmet>

            <h1 className="d-none">Encendido Alsina</h1>

            <div className="main">
                <div className="intro-slider-container">
                    <OwlCarousel adClass="intro-slider owl-simple owl-nav-inside" data-toggle="owl" carouselOptions={ introSlider } >
                        { data.map( ( slider, index ) =>
                            <IntroSlider slider={ slider } key={ `introSlider_${index}` }  />
                        ) }
                    </OwlCarousel>

                    <span className="slider-loader"></span>
                </div>

                <div className="mb-4"></div>

                <div className="container">
                    <h2 className="title text-center mb-2">Marcas</h2>

                    <div className="cat-blocks-container">
                        <div className="row">
                            { marcas.map( ( cat, index ) =>  

                           
                                <div className="col-6 col-sm-4 col-lg-2" key={ `popular_${index}` } >
                                    <div className="cat-block">
                                        <div className="position-relative">
                                            <div className="lazy-overlay bg-3"></div>

                                            
                                            <Link  to={{pathname: `${process.env.PUBLIC_URL}/catalogo/list`, pathImgMarca: cat.pathImg}}> {/* LE PASO AL COMPONENTE  src\components\pages\catalogo\index.jsx , EL PATH DE LA IMAGEN DE LA MARCA*/}
                                                <LazyLoadImage
                                                    src={ `${process.env.PUBLIC_URL}/assets/images/home/cats/${cat.pathImg}.jpg` + '?' + Date.now() }
                                                    alt="cat"
                                                    width={ 100 }
                                                    height={ 100 }
                                                    effect="blur"
                                                    // key={Date.now()}
                                                />
                                            </Link>
                                        </div>
                                      
                                    </div>
                                </div>
                            
                            ) }
                        </div>
                    </div>
                </div>

               
              
              
            </div>


        </>
    )
}