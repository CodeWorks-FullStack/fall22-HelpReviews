<template>
  <div class="container-fluid" v-if="restaurant">
    <div class="row bg-image align-items-center">
      <div class="col-12">
        <h1 class="ms-4">{{ restaurant.name }}</h1>
        <button class="btn ms-4 btn-danger text-light px-5 py-2 fs-5 mt-2">Shutdown</button>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-12 d-flex justify-content-between p-4">
        <h2>Reports:</h2>
        <h2>
          <i v-for="r in restaurant.reportCount" :key="r" class="mdi mdi-rodent mdi-spin"></i>
        </h2>
      </div>
      <div class="col-12 p-4">
        <button class="btn btn-success fs-5 px-4 py-2" data-bs-toggle="modal" data-bs-target="#report-modal">Submit
          report</button>
      </div>
    </div>
  </div>
  <div v-else class="container-fluid">
    <div class="row">
      <div class="col-12 text-center">
        <i class="mdi mdi-loading mdi-spin fs-1"></i>
        <i class="mdi mdi-loading mdi-spin mx-4 fs-1"></i>
        <i class="mdi mdi-loading mdi-spin fs-1"></i>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import { restaurantsService } from '../services/RestaurantsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const route = useRoute()
    async function getRestaurantById() {
      try {
        await restaurantsService.getRestaurantById(route.params.restaurantId)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }
    onMounted(() => {
      getRestaurantById()
    })
    return {
      restaurant: computed(() => AppState.restaurant),
      img: computed(() => `url(${AppState.restaurant?.imgUrl})`)
    }
  }
}
</script>


<style lang="scss" scoped>
.bg-image {
  min-height: 50vh;
  background-image: v-bind(img);
  background-size: cover;
  background-position: center;
}

h1 {
  text-shadow: 1px 1px 2px rgb(255, 255, 255);
}
</style>