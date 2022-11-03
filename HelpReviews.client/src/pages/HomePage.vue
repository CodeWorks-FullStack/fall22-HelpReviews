<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 text-center mt-3">
        <h2>Restaurants</h2>
      </div>
      <div class="col-12 px-5 mb-4" v-for="r in restaurants" :key="r.id">
        <RestaurantCard :restaurant="r" />
      </div>
    </div>
  </div>
</template>

<script>
import { logger } from '../utils/Logger.js';
import { restaurantsService } from '../services/RestaurantsService.js';
import Pop from '../utils/Pop.js';
import { onMounted } from 'vue';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';

export default {
  setup() {
    async function getAllRestaurants() {
      try {
        await restaurantsService.getAllRestaurants()
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }
    onMounted(() => {
      getAllRestaurants()
    })
    return {
      restaurants: computed(() => AppState.restaurants)
    }
  }
}
</script>

<style scoped lang="scss">

</style>
