<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="bg-dark text-light">
    Made with 💖 by CodeWorks
  </footer>

  <ModalComponent id="restaurant-modal">
    <RestaurantForm />
  </ModalComponent>
  <ModalComponent id="report-modal">
    <ReportForm />
  </ModalComponent>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import ModalComponent from './components/ModalComponent.vue'
import Navbar from './components/Navbar.vue'
import ReportForm from './components/ReportForm.vue'
import RestaurantForm from './components/RestaurantForm.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, ModalComponent, RestaurantForm, ReportForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
