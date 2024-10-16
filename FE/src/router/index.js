import { createRouter, createWebHistory } from 'vue-router'

import MainLayout from '../components/layout/MainLayout.vue'
import Dashboard from '../views/dashboard/Dashboard.vue'
import EmployeeList from '../views/employee/employee-list/EmployeeList.vue'
import PredictList from '../views/predict/PredictList.vue'
import UserList from '../views/user/UserList.vue'

import ImportLayout from '../components/layout/import-layout/ImportLayout.vue'
import EmployeeImport from '../views/employee/employee-import/EmployeeImport.vue'
import TTANHPageNotFound from '../components/layout/TTANHPageNotFound.vue'

import LoginScreen from '../views/auth/LoginScreen.vue'
import RegisterScreen from '../views/auth/RegisterScreen.vue'
import store from '../store'

const routes = [
  {
    path: '/',
    redirect: '/app/predict-management'
  },
  {
    path: '/login',
    name: 'login-page',
    component: LoginScreen,
    meta: {
      noRequiresAuth: true
    }
  },
  {
    path: '/register',
    name: 'register-page',
    component: RegisterScreen,
    meta: {
      noRequiresAuth: true
    }
  },
  {
    path: '/forgot-password',
    name: 'forgot-password-page',
    component: LoginScreen,
    meta: {
      noRequiresAuth: true
    }
  },
  {
    path: '/app',
    name: 'app',
    component: MainLayout,
    meta: {
      noRequiresAuth: true
    },
    children: [
      {
        path: '/app/dashboard',
        name: 'dashboard-app',
        component: Dashboard
      },
      {
        path: '/app/employee',
        name: 'employee-app',
        component: EmployeeList
      },
      {
        path: '/app/predict-management',
        name: 'predict-management-app',
        component: PredictList
      },
      {
        path: '/app/report-management',
        name: 'report-management-app',
        component: EmployeeList
      },
      {
        path: '/app/user-management',
        name: 'user-management-app',
        component: UserList
      }
    ]
  },
  {
    path: '/import',
    name: 'import',
    component: ImportLayout,
    children: [
      {
        path: '/app/employee/import',
        name: 'employee-import',
        component: EmployeeImport
      }
    ]
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: TTANHPageNotFound
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

router.beforeEach((to, from, next) => {
  store.dispatch('checkSession').then(() => {
    if (!to.matched.some((record) => record.meta.noRequiresAuth)) {
      if (!store.state.isLoggedIn) {
        next({ path: '/login' })
      } else {
        next()
      }
    } else {
      if (store.state.isLoggedIn && to.path === '/login') {
        next({ path: '/app/employee' })
      } else {
        next()
      }
    }
  })
})

export default router
