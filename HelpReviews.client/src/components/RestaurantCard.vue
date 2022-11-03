<template>
  <div class="row bg-light elevation-2 rounded p-3">
    <div class="col-4">
      <img :src="restaurant.imgUrl" :alt="restaurant.name" class="img-fluid">
    </div>
    <div class="col-8 d-flex flex-column justify-content-between">
      <div class="d-flex justify-content-between">
        <router-link :to="{ name: 'Restaurant', params: { restaurantId: restaurant.id } }">
          <h3>
            {{ restaurant.name }}
          </h3>
        </router-link>
        <h3>
          🐀 {{ restaurant.reportCount }}
        </h3>
      </div>
      <div class="row mt-2" v-if="restaurant.tags.length">
        <div v-for="(t, index) in restaurant.tags" :key="index"
          class="col-2 bg-dark py-1 rounded px-3 text-center text-light me-1">
          {{ t }}
        </div>
      </div>
      <div class="mt-5">
        <p>{{ restaurant.description }}</p>
      </div>
      <div class="d-flex justify-content-end">
        <button @click="removeRestaurant()" v-if="restaurant.creatorId == account.id" type="button"
          class="btn btn-outline-dark">
          <i class="mdi mdi-delete fs-2"></i>
        </button>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { Restaurant } from '../models/Restaurant.js';
import { restaurantsService } from '../services/RestaurantsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  props: {
    restaurant: {
      type: Restaurant,
      required: true
    }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      async removeRestaurant() {
        try {
          if (await Pop.confirm()) {
            await restaurantsService.removeRestaurant(props.restaurant.id)
          }
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
img {
  width: 100%;
  height: 30vh;
  object-fit: cover;
}
</style>