import { createRouter, createWebHistory } from 'vue-router'
import CompetitionListView from '../views/CompetitionListView.vue'
import AboutView from '../views/AboutView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'competition-list',
      component: CompetitionListView
    },
    {
      path: '/about',
      name: 'about',
      component: AboutView
    }
  ]
})

export default router
