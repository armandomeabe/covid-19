var BannerVM = (function () {
    var $main = document.querySelector('main');

    var BannerVM = {
        el: document.querySelector(".sidebar-main"),
        state: {
            title: document.querySelector('.mainTitleLarge'),
            subtitle: document.querySelector('#subtitle')
        }
    };

    // Iniciamos el viewmodel principal
    NFI({
        sidebar: {
            icon: {
                src: 'https://natalfwk.gruposancorseguros.com/nf-institucional/2.2.0/media/Prevencion-salud-logo-completo-blanco.svg',
                href: '#'
            },
            user: {
                name: "Perez gonzalez",
                data: [
                    { name: 'Afiliado NÂ°', value: '98990089' },
                    { name: 'Plan', value: 'A6' }
                ]
            },
            actions: [
                { icon: 'fa fa-comments', title: 'Chat', label: 'Chat', action: function () { console.log('Chat'); } },
                { icon: 'fa fa-home', title: 'Home', label: 'Home', action: function () { console.log('Home'); } },
                { icon: 'fa fa-sign-out', title: 'Salir', label: 'Salir', action: function () { console.log('Salir'); } }
            ],
            links: [
                {
                    text: 'Plan de salud', children: [
                        { text: 'Datos Personales', url: 'https://www.gruposancorseguros.com/ar' },
                        { text: 'Credenciales', url: 'https://www.gruposancorseguros.com/ar' },
                        { text: 'Detalle del plan', url: 'https://www.gruposancorseguros.com/ar' },
                        { text: 'Solicitud de afiliaciÃ³n', url: 'https://www.gruposancorseguros.com/ar' }
                    ]
                },
                { text: 'Cartilla mÃ©dica', url: 'https://www.gruposancorseguros.com/ar' },
                { text: 'Autorizaciones', url: 'https://www.gruposancorseguros.com/ar' },
                { text: 'Reintegros', url: 'https://www.gruposancorseguros.com/ar' },
                {
                    text: 'FacturaciÃ³n', children: [
                        { text: 'Consulta de factura', url: 'https://www.gruposancorseguros.com/ar' },
                        { text: 'DÃ©bito automÃ¡tico', url: 'https://www.gruposancorseguros.com/ar' },
                        { text: 'Medios de pago', url: 'https://www.gruposancorseguros.com/ar' },
                        { text: 'AutorizaciÃ³n de medios de pago', url: 'https://www.gruposancorseguros.com/ar' }
                    ]
                },
                { text: 'Medicamentos', url: 'https://www.gruposancorseguros.com/ar' },
                {
                    text: 'Historial MÃ©dico', children: [
                        { text: 'Antecedentes', url: 'https://www.gruposancorseguros.com/ar' },
                        { text: 'Atenciones', url: 'https://www.gruposancorseguros.com/ar' },
                        { text: 'Estudios', url: 'https://www.gruposancorseguros.com/ar' },
                        { text: 'Medicamentos', url: 'https://www.gruposancorseguros.com/ar' }
                    ]
                },
                { text: 'Beneficios', url: 'https://www.gruposancorseguros.com/ar' },
                { text: 'Oficinas', url: 'https://www.gruposancorseguros.com/ar' },
                { text: 'PrevenciÃ³n ART', url: 'https://www.gruposancorseguros.com/ar/es/home-art', titleAttr: 'Prevencion art', targetAttr: '_blank' },
                { text: 'Llamando al doctor', url: '#llamando-doctor' },
                {
                    text: 'Gestiones', children: [
                        { text: 'Autorizaciones', url: '#gestiones/autorizaciones' },
                        { text: 'Reintegros', url: '#gestiones/reintegros' }
                    ]
                }
            ]
        }
    }, "#app", ['nf-sidebar', 'nf-block-UI']).then(addRouter);

    function addRouter() {
        NFI.Router([
            {
                path: '/',
                template: '#home-view',
                onActive: function (el) {
                    NFI({
                        banner: {
                            button: {
                                action: function () {
                                    NFI.Notification.success('Hola NFI');
                                }
                            }
                        }
                    }, el, ['nf-button-card', 'nf-banner-card', 'nf-info-card-horizontal']);
                    BannerVM.state.title.innerText = 'Seleccioná lo que necesitás hacer o buscar';
                    BannerVM.state.subtitle.innerText = 'Estas son sólo las principales opciones, no olvides mirar el menú';
                }
            },
            {
                path: '/llamando-doctor',
                children: [
                    {
                        path: '',
                        template: '#llamando-view',
                        action: function (el) {
                            NFI.BlockUI.Element.block(el, undefined, true);

                            return new Promise(function (resolve) {
                                setTimeout(function () {
                                    NFI.BlockUI.Element.unBlock(el);
                                    resolve(
                                        '<div id="llamando-view" class="hide">' +
                                        '<a href="#llamando-doctor/vista1">Vista 1</a>' +
                                        '<a href="#llamando-doctor/vista2">Vista 2</a>' +
                                        '</div>' +
                                        '<div id="vista1-view" class="hide">' +
                                        '<h1>Vista 1</h1>' +
                                        '</div>' +
                                        '<div id="vista2-view" class="hide">' +
                                        '<h1>Vista 2</h1>' +
                                        '</div>' +
                                        '<script>' +
                                        'alert("vista async con script");' +
                                        '<\/script>'
                                    );
                                }, 1000);
                            });
                        }
                    },
                    {
                        path: '/vista1',
                        template: '#vista1-view'
                    },
                    {
                        path: '/vista2',
                        template: '#vista2-view'
                    }
                ]
            },
            {
                path: '/gestiones',
                children: [
                    {
                        path: '',
                        template: '#gestiones-view',
                        onActive: function (el) {
                            NFI({}, el, ['nf-button-card']);
                            BannerVM.state.title.innerText = 'Gestiones';
                            BannerVM.state.subtitle.innerText = 'Gestiona todos los tramites que tenemos para ofrecerte';
                        }
                    },
                    {
                        path: '/reintegros',
                        template: '#gestiones-reintegros-view'
                    },
                    {
                        path: '/autorizaciones',
                        children: [
                            {
                                path: '',
                                template: '#gestiones-autorizaciones-view',
                                onActive: function (el) {
                                    NFI({}, el, ['nf-button-card']);
                                }
                            },
                            {
                                path: '/gestionar',
                                children: [
                                    {
                                        path: '',
                                        template: '#gestiones-autorizaciones-gestionar-view',
                                        action: function (el) {
                                            NFI.BlockUI.Element.block(el);
                                            return $.ajax('./app-async-view.html').
                                                then(function (resp) {
                                                    NFI.BlockUI.Element.unBlock(el);
                                                    return resp;
                                                });
                                        },
                                        onActive: 'GestionarVM.onActive'
                                    },
                                    {
                                        path: '/new',
                                        template: '#gestionar-autorizaciones-gestiones-new-view'
                                    }
                                ]
                            },
                            {
                                path: '/verificar',
                                template: '#verificar-view',
                                action: function () {
                                    return '<div id="verificar-view"><h1>verificar</h1></div>';
                                }
                            },
                            {
                                path: '/consultar',
                                template: '#consultar-view',
                                action: function () {
                                    return '<div id="consultar-view"><h1>consultar</h1></div>';
                                }
                            }
                        ]
                    }
                ]
            },
            {
                path: '/cartilla',
                template: '#cartilla-view',
                action: function () { return '<div id="cartilla-view"><h1>Cartilla</h1></div>'; }
            }
        ], $main);
    }

    if ('serviceWorker' in navigator) {
        navigator.serviceWorker.register('sw-app.js')
            .then(function (reg) {
                console.log('SW registrado', reg.scope);

                reg.onupdatefound = function onUpdateFound() {
                    const installingWorker = reg.installing;

                    installingWorker.onstatechange = function reloadOnControlled() {
                        if (installingWorker.state === 'installed' &&
                            navigator.serviceWorker.controller) {
                            var result = confirm("Existe una nueva version de la aplicacion. Desea actualizar?");

                            if (result != null) {
                                location.reload();
                            }
                        }
                    };
                };
            })
            .catch(function (err) {
                console.log('SW no registrado', err);
            });

        window.addEventListener('beforeinstallprompt', function (e) {
            console.log('before install');
            e.userChoice.then(function (choiceResult) {
                console.log(choiceResult.outcome);

                if (choiceResult.outcome === 'dismissed') {
                    console.log('User cancelled home screen install');
                } else {
                    console.log('User added to home screen');
                }
            });
        });
    }

    return BannerVM;
})();